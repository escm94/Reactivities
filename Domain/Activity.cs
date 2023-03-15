namespace Domain
{
    public class Activity
    {
        // if we type "prop" here, we can right-click select what's called an automatically implemented property (C# 3.0 or higher)
        // it'll auto-generate a property like below, and we changed our type from int to Guid        
        // we're going to be using Entity Framework soon, which uses conventions so naming is important here. needs to be labeled
        // Id so that Entity framework will recognize that this should be the primary key to the DB table. 
        // technically, we could get around that by specifying the attribute [key] here
        // Entity framework needs these all to be public. they need getters and setters.    
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }
    }
}