using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        public List<Movy> GetAllMovies()
        {
            MoviesEntities db = new MoviesEntities();
            List<Movy> movies = db.Movies.ToList();

            return movies;
        }

        public List<Movy> GetCategoryMovies(string category)
        {
            MoviesEntities db = new MoviesEntities();
            List<Movy> movies = (from c in db.Movies
                                 where c.Category == category
                                 select c).ToList();

            return movies;
        }

        public Movy GetRandomMovie()
        {
            MoviesEntities db = new MoviesEntities();

            Random random = new Random();
            int pick = random.Next(1, (db.Movies.Count() + 1));

            Movy movie = (from c in db.Movies
                                 where c.Id == pick
                                 select c).Single();
            return movie;
        }

        public Movy GetRandomMovieFromCategory(string category)
        {
            MoviesEntities db = new MoviesEntities();

            List<Movy> movies = (from c in db.Movies
                                 where c.Category == category
                                 select c).ToList();
            int many = movies.Count();
            Random random = new Random();
            int pick = random.Next(0, many);

            return movies[pick];
        }

        public List<Movy> GetKeywordTitle(string keyword)
        {
            MoviesEntities db = new MoviesEntities();
            List<Movy> movies = (from c in db.Movies
                                 where c.Title.Contains(keyword)
                                 select c).ToList();

            return movies;
        }

        public Movy GetSpecificMovie(string title)
        {
            MoviesEntities db = new MoviesEntities();

            Movy movie = (from c in db.Movies
                          where c.Title.ToLower() == title.ToLower()
                          select c).Single();
            return movie;
        }

        public List<Movy> GetRandomMovies(int quantity)
        {
            MoviesEntities db = new MoviesEntities();

            Random random = new Random();
            int pick = 0;

            List<Movy> movies = new List<Movy>();
            for (int i = 0; i < quantity; i++)
            {
                pick = random.Next(1, (db.Movies.Count() + 1));

                Movy movie = (from c in db.Movies
                              where c.Id == pick
                              select c).Single();
                movies.Add(movie);
            }

            return movies;
        }

        public List<string> GetListOfCategories()
        {
            MoviesEntities db = new MoviesEntities();

            string category = (from c in db.Movies
                               where c.Id == 1
                          select c.Category).Single();
            List<string> categories = new List<string>();
            categories.Add(category);

            for (int i = 1; i < db.Movies.Count(); i++)
            {
                string picked = (from c in db.Movies
                                 where c.Id == i
                                 select c.Category).Single();
                for (int x = 0; x < categories.Count; x++)
                {
                    if (!categories.Contains(picked))
                    {
                        categories.Add(picked);
                    }
                    else
                    {
                        continue;
                    }
                }
                                 
            }
            return categories;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
