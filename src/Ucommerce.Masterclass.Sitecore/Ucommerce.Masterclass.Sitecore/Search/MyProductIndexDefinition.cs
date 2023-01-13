using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ucommerce.Search.Extensions;

namespace Ucommerce.Masterclass.Sitecore.Search
{
    //any changes/updates to the index defintion, lucene needs re-indexing in ucommerce section on the cm server
    //Location: (Ucommerce > Settings > Search > Index Everything From Scratch)

    public class MyProductIndexDefinition : Ucommerce.Search.Definitions.DefaultProductsIndexDefinition
    {
        public MyProductIndexDefinition() : base()
        {
            this.Field(p => p["Colour"], typeof(string))
                .DisplayName("en-US", "Color")
                .DisplayName("en-UK", "Kolor")
                .DisplayName("Colours") //fallback display name
                .Facet(); //The field is now a faceted field and appears as a filter when displaying the product

            this.Field(p => p["Material"], typeof(string))
                .DisplayName("en-UK", "Materialo")
                .DisplayName("Materialaaa")
                .Facet();

            //actually is a string type in the ucommerce section on server
            //but we may want to split a tag value into separate strings, so specify type as IEnumerable
            //For Ex: Cotton Fit Shirt into Cotton,Fit and Shirt
            //needs to be separated by pipe symbol in ucommerce section as Cotton|Fit|Shirt
            this.Field(p => p["Tag"], typeof(IEnumerable<string>))
                .DisplayName("Tag")
                .Facet();

            this.Field(p => p["Description"], typeof(string))
                .DisplayName("en-US", "Desc")
                .DisplayName("en-UK", "Descrptn")
                .DisplayName("Desc")
                .Facet();

            //autorange is only use for range of price 19 euro ti 200 euro (purely display purpose)
            //specify precision. pure numbers, no decimals
            this.Field(p => p.PricesInclTax["EUR 15 pct"]).Facet().AutoRanges(5, 10);
        }
    }
}