using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace RustEssentials.Plugins
{
    public class Gather
    {
        static int cachedGather;
        static int cachedAmount;
        static int cachedMinAmount;

        static FieldInfo startingtotal;

        public static int GathermetalOreMultiplier = 3;
        public static int GathersulfurOreMultiplier = 3;
        public static int GatherstonesMultiplier = 3;
        public static int GatherwoodpileMultiplier = 10;
        public static int GatheranimalMultiplier = 2;

        public static Dictionary<string, object> resourceMultiplier = GetResourceList();

        private static Dictionary<string, object> GetResourceList()
        {
            var resourcelist = new Dictionary<string, object>();
            resourcelist.Add("Blood", 1);
            resourcelist.Add("Animal Fat", 1);
            resourcelist.Add("Sulfur Ore", 1);
            resourcelist.Add("Metal Ore", 1);
            resourcelist.Add("Stones", 1);
            resourcelist.Add("Wood", 1);
            resourcelist.Add("Cloth", 1);
            resourcelist.Add("Raw Chicken Breast", 1);
            resourcelist.Add("Leather", 1);
            return resourcelist;
        }
        public static void Start()
        {
            startingtotal = typeof(ResourceTarget).GetField("startingTotal", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }
        public static void ResourceNode(ResourceTarget resource)
        {
            cachedGather = 1;
            cachedAmount = 1;
            cachedMinAmount = 1;

            switch (resource.type)
            {
                case ResourceTarget.ResourceTargetType.Animal:
                    cachedGather = GatheranimalMultiplier;
                    break;
                case ResourceTarget.ResourceTargetType.Rock2:
                    cachedGather = GathermetalOreMultiplier;
                    break;
                case ResourceTarget.ResourceTargetType.Rock3:
                    cachedGather = GatherstonesMultiplier;
                    break;

                case ResourceTarget.ResourceTargetType.Rock1:
                    cachedGather = GathersulfurOreMultiplier;
                    break;
                case ResourceTarget.ResourceTargetType.WoodPile:
                    cachedGather = GatherwoodpileMultiplier;
                    break;
                default:
                    break;
            }
            foreach (ResourceGivePair resourceavaible in resource.resourcesAvailable)
            {
                if (resourceMultiplier.ContainsKey(resourceavaible.ResourceItemName))
                {
                    resourceavaible.amountMin *= (int)resourceMultiplier[resourceavaible.ResourceItemName];
                    resourceavaible.amountMax *= (int)resourceMultiplier[resourceavaible.ResourceItemName];
                    resourceavaible.CalcAmount();
                }
            }
            resource.gatherEfficiencyMultiplier = cachedGather;
            startingtotal.SetValue(resource, resource.GetTotalResLeft());
        }

        }
    }
}
      
     