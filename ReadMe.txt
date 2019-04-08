The project is developed for searching both in the Web and Database. 

When you first run the solution, you see the field where you should enter your query and two buttons: Search and a button that redirects you to the database search page. So, if you entered a query and pressed search button you should see the first 10 results that returned eather Google.com or Bing.com (depending on which of the responces we got earlier). 
There is also added a parser for yandex.com, but due to the fact that it was blocked in Ukraine, I decided not to use this site for my project.

If you click on DataBase search button, you will be redirected to the page with the same form but on that page you can get results of searching among the saved in our db results.

Database creates every time for application starts. To change this, open SearchEngineDbInitializer, and change "DropCreateDatabaseAlways<SearchEngineDbContext>" on "DropCreateDatabaseIfModelChanges<SearchEngineDbContext>".

In order to add a search engine to the list you should follow the steps:
1) add a corespondent property to the SearchSites class in SearchEngine.Core/Models
2) assign a URL to the property in constructor located in SearchEngine/HomeController 
3) add a task ( tasks.Add(website.LoadFromWebAsync(links.YourUri + request.Body)); ) in SearchEngine.Core/Services/CommonSearchService/LookupAsync
4) add your parser method to SearchEngine.Core/Services/Parser
5) add a proper condition in SearchEngine.Core/Services/CommonSearchService/LookupAsync like:
else if (responce.Id == tasks[3].Id)
                result = Parser.ParseYourSearchResult(doc);


Hope you'll enjoy :) 