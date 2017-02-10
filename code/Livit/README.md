1. <b>Prequisite</b>
    .NET Framework core sdk version 1.0.0-preview2-003131 - Link download https://www.microsoft.com/net/download/core#/lts.

    Additionals (options): Visual studio 2015 (udpate 3) with .net core tools 2 - Link download https://blogs.msdn.microsoft.com/visualstudio/2016/06/27/visual-studio-2015-update-3-and-net-core-1-0-available-now/.

2. <b>Install </b>

   Open solution in Visual Studio 2015.

   Open "View -> Other Windows -> Package Console".

   Select Default project is "src\Livit.Data.EntityFramework".

   Run command "update-database" and hit "Enter" to migrate the database.

   Open the command line and navigate to the source project (The directory that contains the README file).

   Run command "window_run.cmd" to build and run the web application.

3. <b> Test </b>
    The web is run under uri "http://localhost:64493/" 

    Swagger test under uri "http://localhost:64493/swagger/"