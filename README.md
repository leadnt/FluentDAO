# FluentDAO
a dotnet database orm framework.
forked from FluentData(3.0.0).


Table of Contents
-----------------

-   [Getting started](#GettingStarted)
    -   [Requirements](#Requirements)
    -   [Supported databases](#SupportedDatabases)
    -   [Installation](#Installation)

-   [Core concepts](#CoreConcepts)
    -   [DbContext](#DbContext)
    -   [DbCommand](#DbCommand)
    -   [Events](#Events)
    -   [Builders](#Builders)
    -   [Mapping](#Mapping)
    -   [When should you dispose?](#Dispose)

-   [Code samples](#CodeSamples)
    -   [Create and initialize a DbContext](#InitDbContext)
    -   [Query for a list of items](#Query)
    -   [Query for a single item](#QuerySingle)
    -   [Query for a scalar value](#QueryValue)
    -   [Query for a list of scalar values](#QueryValues)
    -   [Parameters](#Parameters)
    -   [Mapping](#CodeSamplesMapping)
    -   [Multiple result sets](#MultiResultSets)
    -   [Select data & Paging](#SelectData)
    -   [Insert data](#InsertData)
    -   [Update data](#UpdateData)
    -   [Delete data](#DeleteData)
    -   [Stored procedures](#StoredProcedures)
    -   [Transactions](#Transactions)
    -   [Entity factory](#EntityFactory)

Contents
========

Getting started
---------------

**Requirements**

-   .NET 4.5.


**Supported databases**

-   MS SQL Server using the native .NET driver.
-   MS SQL Azure using the native .NET driver.
-   MS Access using the native .NET driver.
-   MS SQL Server Compact 4.0 through the [Microsoft SQL Server Compact 4.0 driver](http://www.microsoft.com/download/en/details.aspx?id=17876).
-   Oracle through the [ODP.NET driver](http://www.oracle.com/technetwork/topics/dotnet/index-085163.html).
-   MySQL through the [MySQL Connector .NET driver](http://www.mysql.com/downloads/connector/net/).
-   SQLite through the [SQLite ADO.NET Data Provider](http://system.data.sqlite.org/).
-   PostgreSql through the [Npgsql](http://pgfoundry.org/projects/npgsql/) provider.
-   IBM DB2


**Installation**
If you are using NuGet:

-   Search for FluentDAO and click Install.

If you are not using NuGet:

1.  Download the zip with the binary files.
2.  Extract it, and copy the files to your solution or project folder.
3.  Add a project reference to FluentDAO.dll.

Core concepts
-------------

**DbContext**
This class is the starting point for working with FluentDAO. It has properties for defining configurations such as the connection string to the database, and operations for querying the database.

**DbCommand**
This is the class that is responsible for performing the actual query against the database.

**Events**
The DbContext class has support for the following events:

-   OnConnectionClosed
-   OnConnectionOpened
-   OnConnectionOpening
-   OnError
-   OnExecuted
-   OnExecuting

By using any of these then you can for instance write to the log if an error has occurred or when a query has been executed.

**Builders**
A builder provides a nice fluent API for generating SQL for insert, update and delete queries.

**Mapping**
FluentDAO can automap the result from a SQL query to either a dynamic type (new in .NET 4.0) or to your own .NET entity type (POCO - Plain Old CLR Object) by using the following convention:

Automap to an entity type:

1.  If the field name does not contain an underscore ("_") then it will try to try to automap to a property with the same name. For instance a field named "Name" would be automapped to a property also named "Name".
2.  If a field name does contain an underscore ("*") then it will try to map to a nested property. For instance a field named "Category*Name" would be automapped to the property "Category.Name".

If there is a mismatch between the fields in the database and in the entity type then the alias keyword in SQL can be used or you can create your own mapping method. Check the mapping section below for code samples.

Automap to a dynamic type:

1.  For dynamic types every field will be automapped to a property with the same name. For instance the field name Name would be automapped to the Name property.


**When should you dispose?**

-   DbContext must be disposed if you have enabled UseTransaction or UseSharedConnection.
-   DbCommand must be disposed if you have enabled UseMultiResult (or MultiResultSql).
-   StoredProcedureBuilder must be disposed if you have enabled UseMultiResult.

In all the other cases dispose will be handled automatically by FluentDAO. This means that a database connection is opened just before a query is executed and closed just after the execution has been completed.

Code samples
------------

**Create and initialize a DbContext**
The connection string on the DbContext class can be initialized either by giving the connection string name in the *.config file or by sending in the entire connection string.

**Important configurations**

-   IgnoreIfAutoMapFails - Calling this prevents automapper from throwing an exception if a column cannot be mapped to a corresponding property due to a name mismatch.


**Create and initialize a DbContext**
The DbContext can be initialized by either calling ConnectionStringName which will read the connection string from the *.config file:

    public IDbContext Context()
    {
        return new DbContext().ConnectionStringName("MyDatabase",
                new SqlServerProvider());
    }


or by calling the ConnectionString method to set the connection string explicitly:

    public IDbContext Context()
    {
        return new DbContext().ConnectionString(
        "Server=MyServerAddress;Database=MyDatabase;Trusted_Connection=True;", new SqlServerProvider());
    }


**Providers**
If you want to work against another database than SqlServer then simply replace the new SqlServerProvider() in the sample code above with any of the following:
AccessProvider, DB2Provider, OracleProvider, MySqlProvider, PostgreSqlProvider, SqliteProvider, SqlServerCompact, SqlAzureProvider, SqlServerProvider.

**Query for a list of items**
Return a list of dynamic objects (new in .NET 4.0):

    List<dynamic> products = Context.Sql("select * from Product").QueryMany<dynamic>();


Return a list of strongly typed objects:

    List<Product> products = Context.Sql("select * from Product").QueryMany<Product>();


Return a list of strongly typed objects in a custom collection:

    ProductionCollection products = Context.Sql("select * from Product").QueryMany<Product, ProductionCollection>();


Return a DataTable:
See Query for a single item.

**Query for a single item**

Return as a dynamic object:

    dynamic product = Context.Sql(@"select * from Product
                    where ProductId = 1").QuerySingle<dynamic>();


Return as a strongly typed object:

    Product product = Context.Sql(@"select * from Product
                where ProductId = 1").QuerySingle<Product>();


Return as a DataTable:

    DataTable products = Context.Sql("select * from Product").QuerySingle<DataTable>();

Both QueryMany<DataTable> and QuerySingle<DataTable> can be called to return a DataTable, but since QueryMany returns a List<DataTable> then it's more convenient to call QuerySingle which returns just DataTable. Eventhough the method is called QuerySingle then multiple rows will still be returned as part of the DataTable.

**Query for a scalar value**

    int numberOfProducts = Context.Sql(@"select count(*)
                from Product").QuerySingle<int>();


**Query for a list of scalar values**

    List<int> productIds = Context.Sql(@"select ProductId
                    from Product").QueryMany<int>();


**Parameters**
Indexed parameters:

    dynamic products = Context.Sql(@"select * from Product
                where ProductId = @0 or ProductId = @1", 1, 2).QueryMany<dynamic>();


or:

    dynamic products = Context.Sql(@"select * from Product
                where ProductId = @0 or ProductId = @1")
                .Parameters(1, 2).QueryMany<dynamic>();


Named parameters:

    dynamic products = Context.Sql(@"select * from Product
                where ProductId = @ProductId1 or ProductId = @ProductId2")
                .Parameter("ProductId1", 1)
                .Parameter("ProductId2", 2)
                .QueryMany<dynamic>();


Output parameter:

    var command = Context.Sql(@"select @ProductName = Name from Product
                where ProductId=1")
                .ParameterOut("ProductName", DataTypes.String, 100);
    command.Execute();

    string productName = command.ParameterValue<string>("ProductName");


List of parameters - in operator:

    List<int> ids = new List<int>() { 1, 2, 3, 4 };
    dynamic products = Context.Sql(@"select * from Product
                where ProductId in(@0)", ids).QueryMany<dynamic>();


like operator:

    string cens = "%abc%";
    Context.Sql("select * from Product where ProductName like @0",cens);


**Mapping**
Automapping - 1:1 match between the database and the .NET object:

    List<Product> products = Context.Sql(@"select *
                from Product")
                .QueryMany<Product>();


Automap to a custom collection:

    ProductionCollection products = Context.Sql("select * from Product").QueryMany<Product, ProductionCollection>();


Automapping - Mismatch between the database and the .NET object, use the alias keyword in SQL:
Weakly typed:

    List<Product> products = Context.Sql(@"select p.*,
                c.CategoryId as Category_CategoryId,
                c.Name as Category_Name
                from Product p
                inner join Category c on p.CategoryId = c.CategoryId")
                    .QueryMany<Product>();

Here the p.* which is ProductId and Name would be automapped to the properties Product.Name and Product.ProductId, and Category_CategoryId and Category_Name would be automapped to Product.Category.CategoryId and Product.Category.Name.

Custom mapping using dynamic:

    List<Product> products = Context.Sql(@"select * from Product")
                .QueryMany<Product>(Custom_mapper_using_dynamic);

    public void Custom_mapper_using_dynamic(Product product, dynamic row)
    {
        product.ProductId = row.ProductId;
        product.Name = row.Name;
    }


Custom mapping using a datareader:

    List<Product> products = Context.Sql(@"select * from Product")
                .QueryMany<Product>(Custom_mapper_using_datareader);

    public void Custom_mapper_using_datareader(Product product, IDataReader row)
    {
        product.ProductId = row.GetInt32("ProductId");
        product.Name = row.GetString("Name");
    }


Or if you have a complex entity type where you need to control how it is created then the QueryComplexMany/QueryComplexSingle can be used:

    var products = new List<Product>();
    Context.Sql("select * from Product").QueryComplexMany<Product>(products, MapComplexProduct);

    private void MapComplexProduct(IList<Product> products, IDataReader reader)
    {
        var product = new Product();
        product.ProductId = reader.GetInt32("ProductId");
        product.Name = reader.GetString("Name");
        products.Add(product);
    }


**Multiple result sets**
FluentDAO supports multiple resultsets. This allows you to do multiple queries in a single database call. When this feature is used it's important to wrap the code inside a using statement as shown below in order to make sure that the database connection is closed.

    using (var command = Context.MultiResultSql)
    {
        List<Category> categories = command.Sql(
                @"select * from Category;
                select * from Product;").QueryMany<Category>();

        List<Product> products = command.QueryMany<Product>();
    }

The first time the Query method is called it does a single query against the database. The second time the Query is called, FluentDAO already knows that it's running in a multiple result set mode, so it reuses the data retrieved from the first query.

**Select data and Paging**
A select builder exists to make selecting data and paging easy:

    List<Product> products = Context.Select<Product>("p.*, c.Name as Category_Name")
                       .From(@"Product p 
                        inner join Category c on c.CategoryId = p.CategoryId")
                       .Where("p.ProductId > 0 and p.Name is not null")
                       .OrderBy("p.Name")
                       .Paging(1, 10).QueryMany();

By calling Paging(1, 10) then the first 10 products will be returned.

**Insert data**
Using SQL:

    int productId = Context.Sql(@"insert into Product(Name, CategoryId)
                values(@0, @1);")
                .Parameters("The Warren Buffet Way", 1)
                .ExecuteReturnLastId<int>();


Using a builder:

    int productId = Context.Insert("Product")
                .Column("Name", "The Warren Buffet Way")
                .Column("CategoryId", 1)
                .ExecuteReturnLastId<int>();


Using a builder with automapping:

    Product product = new Product();
    product.Name = "The Warren Buffet Way";
    product.CategoryId = 1;

    product.ProductId = Context.Insert<Product>("Product", product)
                .AutoMap(x => x.ProductId)
                .ExecuteReturnLastId<int>();

We send in ProductId to the AutoMap method to get AutoMap to ignore and not map the ProductId since this property is an identity field where the value is generated in the database.

**Update data**
Using SQL:

    int rowsAffected = Context.Sql(@"update Product set Name = @0
                where ProductId = @1")
                .Parameters("The Warren Buffet Way", 1)
                .Execute();


Using a builder:

    int rowsAffected = Context.Update("Product")
                .Column("Name", "The Warren Buffet Way")
                .Where("ProductId", 1)
                .Execute();


Using a builder with automapping:

    Product product = Context.Sql(@"select * from Product
                where ProductId = 1")
                .QuerySingle<Product>();
    product.Name = "The Warren Buffet Way";

    int rowsAffected = Context.Update<Product>("Product", product)
                .AutoMap(x => x.ProductId)
                .Where(x => x.ProductId)
                .Execute();

We send in ProductId to the AutoMap method to get AutoMap to ignore and not map the ProductId since this is the identity field that should not get updated.


**IgnoreIfAutoMapFails**
When read from database,If some data columns not mappinged with entity class,by default ,will throw exception.

if you want ignore the exception, or the property not used for map data table,then you can use the IgnoreIfAutoMapFails(true),this will ignore the exception when read mapping error.

    context.IgnoreIfAutoMapFails(true);



**Ignore Attribute**
And sometimes you need to add some extension property in entity class, when Insert/Update,will be exception,because not find the data column.on this time,you can add the [Ignore](/wikipage?title=Ignore&referringTitle=Documentation) to the special property,like this:

    [Ignore]
    public int TotalCount{get;set;}


this will skip the property when insert or update.

so,if you used to extension entity class,the better way is use both context.IgnoreIfAutoMapFails and Ignore attribute.notice this maybe get wrong data and you can't catch easily.
 
**Insert and update - common Fill method**

    var product = new Product();
    product.Name = "The Warren Buffet Way";
    product.CategoryId = 1;

    var insertBuilder = Context.Insert<Product>("Product", product).Fill(FillBuilder);

    var updateBuilder = Context.Update<Product>("Product", product).Fill(FillBuilder);

    public void FillBuilder(IInsertUpdateBuilder<Product> builder)
    {
        builder.Column(x => x.Name);
        builder.Column(x => x.CategoryId);
    }


**Delete data**
Using SQL:

    int rowsAffected = Context.Sql(@"delete from Product
                where ProductId = 1")
                .Execute();


Using a builder:

    int rowsAffected = Context.Delete("Product")
                .Where("ProductId", 1)
                .Execute();


**Stored procedure**
Using SQL:

    var rowsAffected = Context.Sql("ProductUpdate")
                .CommandType(DbCommandTypes.StoredProcedure)
                .Parameter("ProductId", 1)
                .Parameter("Name", "The Warren Buffet Way")
                .Execute();


Using a builder:

    var rowsAffected = Context.StoredProcedure("ProductUpdate")
                .Parameter("Name", "The Warren Buffet Way")
                .Parameter("ProductId", 1).Execute();


Using a builder with automapping:

    var product = Context.Sql("select * from Product where ProductId = 1")
                .QuerySingle<Product>();

    product.Name = "The Warren Buffet Way";

    var rowsAffected = Context.StoredProcedure<Product>("ProductUpdate", product)
                .AutoMap(x => x.CategoryId).Execute();


Using a builder with automapping and expressions:

    var product = Context.Sql("select * from Product where ProductId = 1")
                .QuerySingle<Product>();
    product.Name = "The Warren Buffet Way";

    var rowsAffected = Context.StoredProcedure<Product>("ProductUpdate", product)
                .Parameter(x => x.ProductId)
                .Parameter(x => x.Name).Execute();


**Transactions**
FluentDAO supports transactions. When you use transactions its important to wrap the code inside a using statement to make sure that the database connection is closed. By default, if any exception occur or if Commit is not called then Rollback will automatically be called.

    using (var context = Context.UseTransaction(true))
    {
        context.Sql("update Product set Name = @0 where ProductId = @1")
                    .Parameters("The Warren Buffet Way", 1)
                    .Execute();

        context.Sql("update Product set Name = @0 where ProductId = @1")
                    .Parameters("Bill Gates Bio", 2)
                    .Execute();

        context.Commit();
    }


**Entity factory**
The entity factory is responsible for creating object instances during automapping. If you have some complex business objects that require special actions during creation, you can create your own custom entity factory:

    List<Product> products = Context.EntityFactory(new CustomEntityFactory())
                .Sql("select * from Product")
                .QueryMany<Product>();

    public class CustomEntityFactory : IEntityFactory
    {
        public virtual object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
