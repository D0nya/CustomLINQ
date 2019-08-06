using System;

namespace Iterator
{
  class Program
  {
    static void Main(string[] args)
    {
      CustomList<string> films = new CustomList<string>();
      films.AddRange(new string [] { "Fear and Loathing in Las Vegas", "Fight Club", "Filth", "Trainspotting", "A Clockwork Orange",
                                     "RocknRolla", "Revolver" , "Three Billboards Outside Ebbing, Missouri",
                                      "Pulp Fiction", "Dazed and Confused", "Lock, Stock and Two Smoking Barrels"});

      var res = films.Where(film => film.Contains(" ")).Map(film => film.ToUpper());
      foreach (var item in res)
      {
        Console.WriteLine(item);
      }
      Console.WriteLine();

      var count = films.Where(film => film.StartsWith("F")).Count();
      Console.WriteLine("Films that start with 'F': {0}", count);
      Console.WriteLine();

      CustomList<string> moreFilms = new CustomList<string>();
      moreFilms.AddRange(new string[] { "Inception", "Back to the Future", "Snatch.", "It's a Wonderful Life" });
      var concat = films.Concat(moreFilms);
      Console.WriteLine("films: {0} + moreFilms: {1} = {2}", films.Count(), moreFilms.Count(), concat.Count());
      Console.WriteLine();

      var filmsArray = concat.ToArray();
      Console.WriteLine(filmsArray.GetType());

      var orderByLength = concat.OrderBy(film => film.Length).ToArray();
      var namesLength = concat.Select(film => film.Length).OrderBy(l=>l).ToArray();
      for(int i = 0; i < orderByLength.Length; i++)
      {
        Console.WriteLine(namesLength[i] + ": " + orderByLength[i] );
      }
    }
  }
}