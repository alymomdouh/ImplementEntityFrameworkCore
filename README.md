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
             
