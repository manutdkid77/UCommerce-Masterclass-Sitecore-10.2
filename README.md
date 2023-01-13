# Learning Ucommerce for Sitecore 10,2

This is a simple Sitecore 10.2 project that follows along with the [Ucommerce for Sitecore MasterClass training course](https://academy.ucommerce.net/training/sitecore/).

## Setup

* This project runs on Sitecore 10.2 and .NET Framework 4.8
* Sitecore Content packages available in the **content packages** folder in the project root. Import them into Sitecore.
* Create a database in sql server called `ucommerce`
* Specify the connection strings in `ConnectionStrings.config` file of your cm server.

    `<add name="ucommerce" connectionString="Data Source=[SQLSERVER_NAME];Initial Catalog=ucommerce;User ID=[username];Password=[password]" />`

## References

### GitHub Repos
[Ucommerce.Masterclass.Sitecore](https://github.com/Ucommercenet/Ucommerce.Masterclass.Sitecore)
[Ucommerce Umbraco](https://github.com/Ucommercenet/Ucommerce.Masterclass)
[Ucommerce.Sitecore](https://github.com/Ucommercenet/Ucommerce.Sitecore)
[Avenue-Clothing-For-Sitecore](https://github.com/Ucommercenet/Avenue-Clothing-For-Sitecore/)


### Doc Links
[Ucommerce Product Definition](https://docs.ucommerce.net/ucommerce/v9.6.1/getting-started/catalog-foundation/product-definitions.html)
[Access Catalog Library](https://docs.ucommerce.net/ucommerce/v9.6.1/getting-started/catalog-foundation/catalog-library.html)
[Indexing](https://docs.ucommerce.net/ucommerce/v9.6.1/search-and-indexing/indexing/indexing-basics.html)
[ProductDefintion Index Location for Sitecore Porjects](https://github.com/Ucommercenet/Avenue-Clothing-For-Sitecore/blob/4450900ebcc44f2b0ac6aa67561d393eeae4f103/src/AvenueClothing.Project.Website/sitecore%20modules/Shell/Ucommerce/Apps/Ucommerce.Search.Lucene.Avenue/Configuration/AvenueProductsIndexDefinition.config)
[Ucommerce Search API](https://docs.ucommerce.net/ucommerce/v9.6.1/search-and-indexing/Search-api.html)
[Faceted Search](https://www.addsearch.com/blog/faceted-search/)
[Search Facet](https://www.loop54.com/blog/what-is-a-search-facet-what-is-a-search-filter)
[Pricing Structure](https://docs.ucommerce.net/ucommerce/v9.6.1/getting-started/catalog-foundation/Pricing-structure.html)