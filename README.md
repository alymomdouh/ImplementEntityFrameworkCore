## Implement Entity Framework Core course content
## video1 introduction
## v2 add dbcontext and connection string
-make new console app(.net core)   <br>
-install packages .<br>
1-Microsoft.EntityFrameworkCore.<br>
2-Microsoft.EntityFrameworkCore.SqlServer<br>
 3-Microsoft.EntityFrameworkCore.Relational<br>
 4-Microsoft.EntityFrameworkCore.Design<br>
 5-Microsoft.EntityFrameworkCore.Tools<br>
## v3 add first migration
            empty migration, add table migration
## v4 save new record             
        save data to dbcontext
## v5 migration rollback 
             remove-migration        ---remove last one    
             update-database -migration:0    --- remove all updates
## v6 add your own migration
   insert into DB by migration 
## v7  Different Between dataAnnotations   vs  fluentApi
## v8 entity framework core -- mark column as required  
             how make   dataAnnotations , and how make  fluentApi in same file or in sperated file 
## v9  entity framework core -- add entity to model 
              domain model (entity)   , ondelete   restrict,cascad 
              3 ways to add  entity class to dbcontext   1-dbset  2- navagation property  3- modelbuilder.Entity<entityname>();             
## v10   entity framework core --  exelude entity from model or from migration    (not make mapping or table for entity)
                            [NotMapped]
                             modelbuilder.Ignore<post>(); 
                             ship all alter for specifc table 
## v11 entity framework core -- change table name 
                            [Table("newtablename")]    //  above class name or entityname
                               modelbuilder.Entity<post> ().ToTable("newtablename"); 
 ## v12 change schema & map model to view   
 ##  v13 entity framework core -- exclude properties 
 ## v14  v14 entity framework core -- change column name
 ## v15 entity framework core -- change column data type
 ## v16 entity framework core -- maximum length 
 ## v17 entity framework core --  column comments 
 ## v18 entity framework core --  set primary key
 ## v19 entity framework core --  change primary key  name
 ## v20 entity framework core -- set composite key
 ## v21  entity framework core -- set default value
 ## v22  entity framework core -- computed columns
 ## v23  entity framework core -- primary key default value
 ## v24   entity framework core -- [one to one relationship]
 ## v25   entity framework core -- one to many relationship 
 ## v26 entity framework core -- one to many relationship  part2
##  v27 entity framework core -- many to many relationship   
##  v28 entity framework core -- Indirect Many To Many Relationship   
##  v29 entity framework core --  Indexes   
##  v30 entity framework core -- Composite Index   
##  v31 entity framework core -- Index Uniqueness   
##  v32 entity framework core --Change Default Index Name  
##  v33 entity framework core -- Index Filter 
##  v34 entity framework core -- Sequences
Sequences is make primary key or autoincrement in multi table 
##  v35 entity framework core -- Data Seeding   
##  v36 entity framework core -- Manage Migration and Generate SQL Scripts
    very important video about migrations and how apply Migrations in server live or move Migration from local to live the most used commands  
            =>script-migration
            => script-migration migrationnametostartfromtoend 
            => script-migration migrationnametostartafter migrationnametoendwithit 
            =>get-migration
            => update-database migrationnametoendwith 
##  v37 entity framework core -- Working With an Existing Database (Database Scaffolding)
     very important video how deal with exists database in entity framework core
       --old ways befor entity framework core
             1-DataBaseFirst 
             2-CodeFirstFromExistsDatabase
       --new ways with entity framework core  
             1-Database Scaffolding(Reverese Engineering)
           -- scaffold-dbContext 'connectiostring' service providers 
           => scaffold-dbContext 'Data Source=.;Initial Catalog=databasename' Microsoft.EntityFrameworkCore.SqlServer 
           // be default will use fluentApi and will generate dbcontext and all models 
          => scaffold-dbContext 'Data Source=.;Initial Catalog=databasename' Microsoft.EntityFrameworkCore.SqlServer -Tables Blog,Post -OutputDir Models  
                        -ContextDir Data -Context newcontextname
                        // to get only needed tables //ToPutTheTables,DbcontextInFFolder  //ToDbcontextInFFolder
                        // to change from fluentApi to dataAnnotations 
           =>scaffold-dbContext 'Data Source=.;Initial Catalog=databasename' Microsoft.EntityFrameworkCore.SqlServer  -DataAnnotations 
##  v38 entity framework core --  Select All Data, Select One Item Using .Find // and give id 
           using website mockaroo.com to get data 
##  v39 entity framework core -- Select One Item Using .Single  
##  v40 entity framework core -- Select One Item Using .First 
##  v41 entity framework core -- Select One Item Using .Last
                last only with orderby or will give exception 
##  v42 entity framework core -- Filtering Data Using .Where 
##  v43 entity framework core -- Any vs .All 
                Any => return true or false  if any row give true of condition
                All => return true or false  if all given rows success the condition =>true else false 
##  v44 entity framework core -- Append vs .Prepend 
                        Append=> add element in last of list 
                        Prepend=> add element in first of list 
                        Append ,Prepend  only applying in client side not server side 
##  v45 entity framework core --  Average vs .Count vs .LongCount vs  .Sum  
##  v46 entity framework core -- Max vs .Min  
##  v47 entity framework core --  Data Sorting Using .OrderBy vs .OrderByDescending vs ThenBy vs ThenByDescending   
##  v48 entity framework core --   Projection Using .Select   
##  v49 entity framework core --   Select Unique Values Using .Distinct   
##  v50 entity framework core --  Take vs .Skip   
   


     
      
                             
