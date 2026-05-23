using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain
{
    public enum  ItemType
    {
        RawMaterial = 0, //Bought from suppliers
        Component = 1,  // manufactured in-house but not sold
        FinishedGood = 2    //sold to customers
    }
}
