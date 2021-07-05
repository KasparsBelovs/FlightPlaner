# FlightPlaner

<h2>This is my Web Api Project:</h2>
To run this project use SQL database or check connection string to moddify for your needs.

 <br/>  <br/>
 In project I am using: <h5>SQL database, .net framework, entity framework, automapper</h5>

 It is api with 2 main controllers: customers and admin (admin with basic authorization).
  <br/>
 This api got endpoints to 6 endpoints: add flight, delete flight, find flight etc
 <br/>
 There are some data validations.

There is 3 main branches in repository
<details><summary>feature/BesWay branch</summary>
<p>
   <br/>
    This is so far the best way how to do it. There is 4 projects: 
    <br/>
  1.  web api project (thats where are controllers and all end points what to give to front end.)
    <br/>
  2.  core project (thats where are models, data transfer objects (DTO) and interfaces for services.)
    <br/>
  3.  data project (thats where are database context and migrations.)
    <br/>
  4.  services project (thats where are implementations for all services.)
</p>
</details>

<details><summary>feature/adding-database branch</summary>
<p>
   <br/>
    This was second stage where I added SQL database, everything was made in 1 project and can't be modiefied easily.
</p>
</details>

<details><summary> feature/in-memory-app branch</summary>
<p>
   <br/>
    This was begging of project where everything was saved in Lists and the goal was just to make it work so I could go to next level and add SQL database.
</p>
</details>

