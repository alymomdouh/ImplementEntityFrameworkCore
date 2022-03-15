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
  
 
                             
