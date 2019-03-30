﻿using System;
using UnityEngine;

namespace Ragnarok.Plugins.Branding
{
    /// <summary>
    /// Author Gintaras/newage_
    /// </summary>
    public class Branding : MonoBehaviour
    {
        public bool ShowMap = false;
        public Texture2D imageLogo;
        public void Start()
        {
            byte[] bytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAPIAAABMCAYAAABXqOqhAAAABmJLR0QA/wD/AP+gvaeTAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAB3RJTUUH4QgMDjs0fPKgeQAAAB1pVFh0Q29tbWVudAAAAAAAQ3JlYXRlZCB3aXRoIEdJTVBkLmUHAAAbGElEQVR42u09PWwa2fPz+PZimySyS8O1SEZXcJ0J7aGga+iM+LcsVppzhWTXtkSVNJa9tD9kdzQWFtcSuwtFRCTaQEpbuZCENd/vX8DsLev9hAXshJGiWDbsvH1vZt58D4ElPBlwhuOUNhsAANApF8jPjncJU4IrskfNOPzlTpoDdn8oiT87glH67KhEnxrex0JT4nf6mcCCP6zt5yhuVKt4StyJYzrNxhHGIxCA1uZNurl2fyj5/O0HOgmROILRRy9oXJE9urafo4TxcPi7TrlA+nd1WD+8pI8d7yJpiomlZfFYfQHup5b6L7Kf6PO3H+j64SV1BKPUFdmj7sTxVMTy7KhE1RgG8Ux6KHqZ0hmO0+dvP1CzJLzdH0rq+a7dH0pOKqTU3snuDyVnxchm4l0ETYnfQ4xrbT83plHgM/SejyuyR7XocFrhOY22INzI7dI5EMYDNl8AXJEUuF7ugjMch7X93MTE3ikXoFMuEKUbs1MuENfLXbD6AtyL7Ce6yp5QKb71w0vK5zNE7SbXstscwVfQv60/+P3gts7a/aEk4lzbz9Hnbz/Qtf2cIGDs/lBSSjiuSIrD2wEPQe4d7f4drlu9zuLfxe+mdWjO8K7s+77IfqKE8XDfjv4iegnEiPZjFt5F0ZToc8QZjsMqe0Kd4Tgd3NaBz2eAiaXpi+wnSvkGu354SZ3hXU4PAw1u60AYjyAA5JhaSwiqntOIDybWVvCHVfaEOoJR1UPp332GwW1dk3HW9nO0U74CwnjAurE19j1nOE5t3m3o332GVvGUuCJ7dCWSAjFjTAI/uNfgiqTAwnigV68AAIDNG4Bu9Rr6d5+BiaWhXTqHZu6AjN+YO5wrsgcAAJRvPFgH5RvQeV8AZzgOg9s6WDa9imvo1Spg3fTKvovcs/WCFG+nXIAf3GuCe23d8IJl0yustVO+YrvV6ywSnM0fApsvMFe8i6Ypu38H7P4Q9GoV6NcqYPeHBFpYiaSANhuqZ6kHerUKdMpX0CqeEhSa/VqFtft3OMumF+z+ENBmAwZ8Y6L91wLKN6BbvYYf3GtCRGqD6pe+JH/TLYldkT06uK2DM7wLg9FNKMcI0xC3GqMoMRyfzwibbveHkoTxcEwsrXmgk65TvI5erWLqYTZzB2DzbkMzd0Dkzg+FFhNLU6svAHZ/aK54F0lTAAD3xTOgfAPapXO0zWdmllK+Af3b+oixbsCMi0kvtIqnwOczxIaOCADQ3HgjBNwpF0inXABHMEo75QJx/BGlA77xny4vcl6YAeJnWTa9Q1XI7QHCeKBbvYZu9UZs6wi3QevdBaD01vNsI4crJjAL40HVzrT3Fd7p5a6cXwD6d58pn8+MbqjQXPEumqZsvm1Be5glEyNOFNJmXk46BRx0qzdJi4izCUqzaUGsJqHq061eK95IZuHFZ/VqFfh6GCZ8PgN8PgOE8cBKJAUrkZTASI4/omD370CreEr6tcpMDncgssunVePk7H4tJlmJpEzfXyN4F0lTuPdr+zlq5vtr+wUu2N4M6Em3s+v52w8U1QOlTZkUmFgaerWPyszXNHej8R2sG1vQKp6Swd3QUSGWlITxADLwgP82k83t1Stje2fu7XQFlk0vrB9eUiUhQRgP+gHmjtcRjNJF0hRhPPAi+4na/aG53pCLAgs6JdQcNQMTJJoj+Ep5ESbfVkgk6O22bMg/3zqS5mYR+oP32vAKjjezBYSFWReca2pg9QU4mzcwd7yr7AkskqYo3wA+n5n4IpjmFh/c1efOyLaRx5fMMsGgN/IaTvrd+3yGHUlZTurYoM0GUL7B4u+61eus3R9K6vHWWje8MFTRbhTXN7itQzN3wFp9Ac7m24Ze7SNQvgHuxDH84F4LuO3+Ha5/91kg2n6tInhw7f6L5EjN0x1e4PMZ6NcqrNzf8B17tY+cM7xrmiPLTLx8PqPKaLOmqW71hl2JpTk1m7VbvQbCeKBVPAPLphf6tQpr9QU4ufdH2nO93FW9eOz+Ha5dumAJ4+GsG174ehgmrsgedb3chft/zsC6sSVoEq5ISpU+f3CvhXWpCDMOQBR+enZUomoLNOJhHEcUpavsiSnEJQ5t6IUX2U9U7ebm8xnolAtE6XPd6jV8f5MgUg+qzbcN7dKF7jXZ/aHkSizNqR1cM3cAK3+mgLg98O/fvxMz3hE9qvPG2ykXwOYNwKJo6vubBOuKpDg1ZjeK3xmO03bpnGjxCjpXVyKpsf10J46pOPy5tp+j064PHX82/EX/rm66iouOiV4tRZVULD2S1+bbBiMOKYwrijdNDvCd5bK+BJVFhgEwhIUMqoeZu9XrrItPceqOknPSLp1PuEcBxb9pO2jMx0sYz6OlKS1wRfao+Iwx9GXd2AK7P5S8/+cM7P6dMdzWkfk2GIWgAIYhMInAJLPYC0G1njV0yldTOXo676/AGd7lrL4Ah3HDwdCJwopVH/f/HQNhPNCrVaBXvdZM1B/c1jWdLr1aZew54uC/2A5Vej5xe4A2G5oJD5N4Oh3BKKV8g7WoELOeJJZF4F00TeGN2K3eCLRk2fRyUkHtDMcpaoOUbySH2Xo37I/SeVZLAGjatSbmFcyFkadaoC8AqEbJZAlxSt/Rs0mYieSKpKjS5+3+0JgtZt3YonZ/SDeRdsoFsPtD0CkXFB0oGEZZ28/R728SBDOECOPhpAJglT3BdEOCYRi1tfTvPgNxexaCd5aMrNvp6Vc1d8RnK9ASE0vT5v8OxDYsR/mGkJbriuxx+Hm8NPh8hhhhYtx/tcw3JpZWTE+emJGd4TgljEfIloEnDu3SOaBKbDVAcEaTCzD8YfeHkkrecyQmuz8E64eXtFO+glX2hBtqFynaKV+NqazIIM5wnLoTx5oe2EXhXTRNdcpXrDMcN5y/TBgPSG1wJRUeLw1XZI82cweG3kPLLNDLxIYYeejdO33yDDwimrHY8TRedfFz5Q4Gf9etXmfdzLEmUUm1CSXtwhG8olpeYWSQlT9TdBF4HcFXdFE0hUK6XTrnZp3ZJXJmAQDQRVx0FrzC5Q5NbD9Os+E23/bcmVXpViCMB1yRPbBubJmGa3BbH3kqh//UnGvmmRzbgoNFaV3oMV0E3sdCU83cAWkVT6d652bu4IFfQUnjMCvpZyIbmc9niCMYFdQcvEXMsnGm9SwObuuyxDhMx/wI1o0tcPwRFfKqKd/QvGGd4Tj06h81pSemfGKlD8Ye8dbqVq+hXbpgxQ6v/tBBJlVjk2bGe6XPlxIaxt7NBr14HxNN8fkM4fMZcIbj1LqxJSQCSR1OSs+0bmwBn8+MhZOUikLMLAIyxMhopzRzB+xKLM2Z7U0zQUUai+MqhXuauQNgYmmKwXabNwADvgEWkW0nhZU/U6An7IL41UJNVl+A+0+ljFKMAeJ3pi2oN+ogdEVSnDtxzH09DJN5413bz3F8PvPoaEpOaEs9zeIzxsoxRzAKXw/Dws3uCEZVzQu9IUm1PPH7f86EGLEu1dru3wHKN8DqC3C90W2m5JhY1I2MHTm0Ngg9qp1ygXw9DJNvR38RNVVXz+0gZnRsEiAuKscwVKt4SjDVU27zm7kD1bWYDXZ/yJQ0yEnwCrnuj4CmXJE9+uyoRJ8dleiL7CfhH2YySsOPYvri8xny/U2CNHMH7IvsJ6F5xJDGrmamVRC3B9yJY0Pai61X+wgrf6Y0CXukRkxkyIslzyRlXt3qdXYtkuO+v0lkpRJPzEgKN6WmkFBbj3RPWsXTUTprBtYPL6l10wvWjS06VLtvDEnkedyQiwDCeICJpTVjybOmKbU6ZJsvAM+OSnQUmlO9+dBsahVPCYaEpk0/1dOVxIh6TQCG7XRaxTPAhAojtmvr3YWwkaha2v2hpGXTy1k3tsAZjo89c5bNBaQq+bAQPa6lNrNWX4BTKwaXy2ASJZ0YWpOanUz5BvzgXsvGcWWED4fMoMYsfD6jWW89C7wAw1zhRdEUn8+AVohsEppC5lKL/35/k9BM3bX7Q0l34phT2sPBbR2MmEVjH1TLnVUiACOHpOfzs84IksKX5G9EK+fVLOeF1rtLWxHpVR3VPKWt4ilodQiZBV5sC7QImsIWOFoMN6kDSm194miBFpiRay1ojlKJM0vbV+7zmCKIpWNKWUizgGlrYo2+u9b+TpKLq1YZA6Cv1noWeNHpuAia6t/WweYNqNYrT3qGWuvrvC+YJjSMtGy2jEvmi7naUpRvwLejv8gP7jXcF890bZSZ0Ho33/ed9a0ve8DM+kLsZMS7KJoaeZnJPLuDUL4hCMVpW+feF88MxaQtYlWJ8g32+5sEa/bLi22LbvVauIWb/xsG2lfZk5kF0gcKHSp6tcrI02xOfNeMPZvFFIQB/01TOM4K7yJpSmSns/OIFvRqlTGVWo95KOeMbJfOoV06h5VIylBjBJvIlsJGZbRXqwixUy0bB4v71YutQ2M/U74hOATMGCWiqgLe1YHWG2O2Uq9WASP9maX2O6Z5itvoIgExsbRgHkzinZ/FHnTKV6oe7FnhXSRNYcUS/ls/vKR6vfh6/TT/ZfTdCCFHnJOFjf+MgjMcN2Rnyzq7xLezNEgu/YzVF+Ds/h3olK+gXTrX1f60W72Gfq0CA/7bWFtaSVYUi90Y0A6Tlgk6gq+gV70W7L+VSArui2fC/2JCsDDrw9th1PGxW70eswnFXTU75StW/H7WjS3hd3LELpTzbXqFyhgLsy4kuzOxNHUEo0I3z2buQLHntZ5SR60b1e7f4UYdTFjsHCF910XgXQRNyZ2XO3FM+3efoV+rjJ2Z2OZHrzyWyCItir3nhPFwrkgKWsWzMQZ2BF9Bu3RhaD/Fa0Lccx+UN217oGlUOvGIDVdkj64fXk48juTZUYlK3wWfrWfOkNY7DjOBZj9rSoxjnkP0zMS7SJqa91qV9nCeTpvkojZPialX2RM66VqUZkPhYU37fsvJlE+Ppn65DX8sa5pUoqkNBZtmCNsSnjZNLeGJgTtx/GDurztxTOdZ5LCEJSzBJFtneSssYQk/CTyF4edLWMISlrCEJSxhCUtYwhKWsIQlLGEJS1jCEpawhCUsYQlLWMISlrCEJSwClpltTwssj32BznCcYgtabIu7qLXMI/vrsWSYWX0BcCeO6dp+js66ZnwJvyBM20LFKIiJeFZEPSkORzBq+nrW9nPUnTimL7Kf6PO3H+jztx/oKntCneE4HdVX68Zn9vpckT3V5y0z854gM0sLHaYl3tGEwbGcazWiQc1AL/GYjUOaH/5fkf9/FVt2fygpbqyu9Cz8/Cp7Qt2JY4FZ8X9cB5aK6tGK9KxPSzNxBKOKdeZr+7kH9ePSvf2VYCHTFeUazFuYdaFtq1ZL1HbpXGiJIvc57Gndq3+UHRPCxNIUh6Xb/CHDjdzV1iceYjYrHADDftWul7vQv6uDzRcA6YwlPp+Bfq3Cru3nOID/+kXb/Ttjs6umAWw7263eCPuMY05ckT2qtT6tIW6uyB5diQyHJ+htmyQdMaQFz99+oPhso+v7ZRlZfMg23zZYNrxg8wWEYeB6D6tTLujuV4zNypHQjPSw7tUqqrOj1Ihp1ji+JH8jz45KqsPGv79JCIw8a5A2lm8VTzXXp6dv8/rhJbUwHqFdkp7zbpfOdc0WXtvPURzON+n6fklnF/YicgRfgV10SzmCUdVDknbCROmuS1Ixwzk6ePMb6Zhp8wWg9e7CUP/rwW19XjiS82zkr0lIm95h48ERE5u1vl71GiybXrgvnuk+b1dkTzAD1EyKYa+vBvwMMHevNc7NNdJh0rLpfTAixGiLUyaWBkcwSo18j/INcL3cNXRbDkRTDmaJQw/ghMh5weC2boo6KvEhAAp/LdxSs0TsG5BraGdVGOa+ZGQdYGRWkspBTzQKRGtIt5x0t2x6UVXT9Z32u4u54NBnyrwy/fxwIoiS3W7kOSqmSVbsXKN8Q5Ph0B/RLp0DbTagW72BVvGU/Cr90ubKyGZtqnRgV6esPaajUy6A1ReYaK6UVafURlt8Hjh07RPjAdpsaNroRqB/W1fUplbZE9AbTdCD16iwtmx6wRmOg2XTC3b/Dqzt5+ikk0voE1O5bfNEZt3Y0uUo6pSvYHBbZ1VuMQ4AhEl/3eoN9GofQWmi4uC2PvRi1z6q2q+9WgXu8xlWDl+nXCA23zZ1BKOCR1juFhGrdrPEoWcftUyYdukc+nef4dvRXwTVWewhPvJLsBJG4WzebWi/u1CcdGiW3S4NL8mdqVYD+3bpfOL5XvOcZf3kGFlroJgBl38Wb1nxVPd+rZKUemm71WtoFc9YZ3iXc2rceqNwSlZmUnwWAEbTBaMUP4dOFPTE49p7tYqiwDALhxaRd6s3QBiPokoq3WvEpSosqpBtj36cJoUT16dmI4v3T27a4tfDsOYUTcumFzoTDKgbCrkLdqlaKx7QjrrapjHhT0FlHiNGqQRuFc+EWbVaahblG+CK7FFneBeeHZWo1POJDCBWV0VChNVjOswDx1B4qQ8w69cq7KTJE1rZWlrPxfUpmV+uSEpXyKxVPGO1fCmTwkoszWF6ML7vY7a358rIWnaHK5LiMAVz0nS7/l1d6vDhmFia2rzqHkrxYO3vbxKk9e5i7NbAtMV+rcLi7SglbC3bdR44AAAcf0Qxs0pRtVyJpTnLppd7kf1EjRJoq3hKvr9JsHL/viR/I1omAHF7YP3wkroie1ScPeYIRilhPJpD2XEuUrd6ndXyj6yyJxOlidp8AbB5t8doVq9v4qdXrXu1j5oOjJEdmzRr/o0zHH9wG4gJBZkLbe2VP1PgCL4SpTQOf0ZPtDtxzA34BmfzBWAlkqKE8UCvVoGBb5tDO1eJgWeJQ2JvC0KrfydvS9pGoZehefIKHMFXFIUKDkDTuFUnHvzmCEZHNvw2/BjdzDjrScvTLo0noymihqvz/moi9V86IlXGHPo1GVkYZq6g4ooHYOFUu1lvnHgtODAMh3bJqeOWTa+gxoylOvoCYNnwynrQ54FDzqlm0bCR9TDsrEBs8oy9u4r5Q/mGrEqOKbtKDjBneBcsm15qJMZt2fQKg+f4fGbMP+EMx+lju53nqloPb6RrtYNixWqM2UxsJNQy6dB1I3bnNDikEyof+g6uHiTSmHmraoHW+tT+rnZOUnPDEYzSZu6AUL6hmChj94fA9XL3QWWZmqlH+QbY/TvgCEbpKntC0UQZDvXb+bVvZJtvW5OwlEZiTgvt0rkgaWcmFRkP4FjRWeOQmgxSUBtHalYGlowaS9E5Z3RwvXiEqkWBsX5wr1mprS4IrvcFIG4P8OUrcARfyWoi0mIKNVt8ODx9OCr2B/c6u354SSnfSKI6/0szspYjwxXZoxZmHfTYgZPYysjMM/MB1CvQzB0QrUHeZuDQ46Dq1SoPCJryjVHCxHQC0xmOU2lFk3VjCxx/RMH9f8ZCU45gVHMOcv+2DlZfgLP5tmElluaG4bkbGNzWoVMuELRnneE47ZSvRjdqaExVlqrE6pfKjWC3E8bD9WsVcARfcc0Jw1k/FSP3VeKrwk1RLhC7P5RcZU/oJJJPLT3SGY5rBvq71WvBsyxOqNAqK0QJj/ncWtJ+Whxa+8DnM8DE0tCrVaD97gLapXOyfnhJkbGn1XpGaqfiORoxGVyjUkV1be5h1MHuD0GnXMDquaTdv8O5InvQzB3I3sgrf6bGhLnaOTlf7kK7dC7c+nr8Er8OI999Vv37KnsClD+m98UzsHkDsk4FtA+VVMNpVGfMAAMAwWPpCEYp5Rssqv1SO4/yDaDN4S3nerkLnXKBOMO7dNY4AE40mbmZOxgLmYgdSdLkEsykEntr1W5QNUFF3B6dwiCUJIyHm6ZwweYdtiQSmxpqWWdr+zmqR2BitpsegecIRqnNt62rdHJm2u48kTGxNNVTNCF2QnSr19B5fwVI6GKiw4QAzJRyhnc1kwDUapmNFqUjMUoPe5U9obPGoaW+Y+aWWBhK64Ol2V2OYJQStwfapfMHRCwOvWhlVKE2oGd9WjXLekAPY4oF6dfDMBHb5Eqf4/MZXVETpL1+rQLd6s1CIgFzZWS7P5TUW+iupzh/cFvXXXCOz+xWbxSJbBIm03LczAqHXkbW851erQJWDQ+3Ecb5wb2GVfZEFyPP0p+gBK3iKfTvPgt18Xr8DOJ37pQL0Kt9BJtvW/i+dD/apXOYpz091/BTt3qd1VNVorc432KA+PC5vxI8f/tBSGRYP7ykavannn3E/dOR6qqZasvE0pr52r1aRag9R7oZFr9UxkJUSjSFlXHSv48cn7qYDNV+8TsTxgMWZh1sXuV9c4bj4E4c03m1FZ57PXLnfUEXg86ijKxTvmL1PBftVHEKoVFVb9E47P4duC+egSuyR1fZE1MK6PWowFjsoWcPtEJo9/kM+/UwTL6/SZB///6dfEn+Rr4ehsngrg73+Qz7Jfkb+ZL8jfz79++Ez2eEtk7N3IHAcIPb+oNSTsJ4QC7H3YBmKZRLqgk2y6ZXtTjkyTIyE0vTbvVmIbWenXIButXrrB5JjDbOpOucB4774pmmhsHE0mA0njutAMEUSj3r03Ne8vt78cAObRVPyb9//05GPbuEghFXZE9WAKm1l9JzJno1GD3ayZNjZD6fIZZNL/z79+9E65AnLQhXIgoMZYmzdBRU+uTIC0onzS6bB45W8ZTc/3OmGBZB04TPZww3DzB6LqMEkzG7XGt9WsJCLfSo5kxCISpNHlGz1aWMK37HaS4d6xzbCM01/DSUhK9gcFun6DlUysKZFtC26pSvxqQ3UelYKa4BnsbzOA8c7sQxdfwRhf5IYEiTPzDvuF+rsN+Kp9lRg3lTBSQKiG9HfxGj69Pz3Gn9MXw+w6lpJDZfAO7zGbZXq3BKfgIjMX4502BeHuyFxL3W9nO0XboQ1D8AgNa7i4nqkUe2CHa1YLUYBJMisGgAu424IiloFc9MKdKYBw7EY2E8cP/PGbRL58QZjlO7fwd6tY9jggNvKfz818MwEcerCePhmFgabydWz34TxqNZdKG2PjGD/eBeC3hnQfiuyB6lfAOsG1vQrd7Idl1xJ46p1ReAwd0wmQXpk7iH7ZIwOiJmaDG9yuWOP6W+2FOB3R9KuiJ7shMJ9H4fbW+t1jBiIaLkwcWaWDME1axxIDw7Kul6HnpPlSZNzKpoXu/6HgMt4v6sH15S6aSMJRhVFw0eut4RJL8yOIJR+vzth7FQCO6bmaN3njo4w3G6yp5QJpami4hvL2EJugSelGl/lVaxk14qy11YwpNQI5fw88D/A5uGNNGgNkw+AAAAAElFTkSuQmCC");
            imageLogo = new Texture2D(242, 76, TextureFormat.RGBA32, false);
            imageLogo.LoadImage(bytes);
        }
        public void OnGUI()
        {
            float num = (float)Screen.width * 0.09453125f;
            float num2 = (float)Screen.height * 0.05277778f;
            GUI.DrawTexture(new Rect((float)Screen.width - num, 0f, num, num2), this.imageLogo);
        }
    }
}