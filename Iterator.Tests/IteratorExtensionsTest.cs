using System;
using Xunit;
using Iterator;

namespace Iterator.Tests
{
  public class IteratorExtensionsTest
  {
    private readonly CustomList<string> films = new CustomList<string>();
    public IteratorExtensionsTest()
    {
      films.AddRange(new string[] { "Fear and Loathing in Las Vegas", "Fight Club", "Filth", "Trainspotting", "A Clockwork Orange",
                                    "RocknRolla", "Revolver", "Three Billboards Outside Ebbing, Missouri",
                                    "Pulp Fiction", "Dazed and Confused", "Lock, Stock and Two Smoking Barrels"});
    }

    [Fact]
    public void All_AllFilmsContainSpace_ReturnFalse()
    {
      var result = films.All(film => film.Contains(" "));

      Assert.False(result);
    }

    [Fact]
    public void Count_FilmsCount_Return11()
    {
      var result = films.Count();

      Assert.Equal(11, result);
    }

    [Fact]
    public void Where_GetAllFilmsWithNameLength18_ReturnCollection()
    {
      var result = films.Where(film => film.Length == 18).ToArray();

      Assert.Collection(result, e1 => e1.Equals("A Clockwork Orange"), e2 => e2.Equals("Dazed and Confused"));
    }

    [Fact]
    public void Map_UpperCaseAllStringsAndGetAllStringsWithAND_ReturnList()
    {
      var result = films.Map(film => film.ToUpper()).Where(film => film.Contains("AND"));

      foreach (var item in result)
      {
        Assert.Contains("AND", item);
      }
    }

    [Fact]
    public void CustomLINQ_CustomLINQConversationsOccureWhenIterate_Return3And4()
    {
      var startWithF = films.Where(film => film.StartsWith("F"));

      var result = startWithF.Count();

      Assert.Equal(3, result);

      films.Add("Fargo");
      result = startWithF.Count();

      Assert.Equal(4, result);
    }

    [Fact]
    public void Concat_ConcatTwoLists_ReturnCountOfConcatenation16()
    {
      CustomList<string> moreFilm = new CustomList<string>();
      moreFilm.AddRange(new string[] { "Inception", "Back to the Future", "Snatch.", "It's a Wonderful Life" });

      var res = films.Concat(moreFilm).Count();

      Assert.Equal(15, res);
    }

    [Fact]
    public void FirstOrDefaul_FirstStringWithLength10_ReturnFightClub()
    {
      var res = films.FirstOrDefault(film => film.Length == 10);

      Assert.Equal(10, res.Length);
      Assert.Equal("Fight Club", res);
    }
    [Fact]
    public void FirstOrDefault_FirstNumberEquals100_ReturnDefault()
    {
      CustomList<int> intList = new CustomList<int>();
      intList.AddRange(new int[] { 1, 2, 4, 5, 6, 7, 8, 10 });

      var res = intList.FirstOrDefault(i => i.Equals(100));

      Assert.Equal(default, res);
    }

    [Fact]
    public void Select_SelectLengthOfStringsAndGetSum_Return()
    {
      CustomList<string> stringList = new CustomList<string>();
      stringList.AddRange(new string[] { "a", "bb", "ccc", "dddd", "eeeee", "ffffff" });

      var lengths = stringList.Select(s => s.Length);
      int res = Sum(lengths);

      Assert.Equal(21, res);
    }
    private int Sum(IIteratable<int> source)
    {
      int sum = 0;
      foreach (int item in source)
      {
        sum += item;
      }
      return sum;
    }

    [Fact]
    public void Some_IIteratableOfStringsContainsSpace_ReturnTrue()
    {
      var res = films.Some(film => film.Contains(" "));

      Assert.True(res);
    }

    [Fact]
    public void Some_IIteratableOfStringsContains000_ReturnFalse()
    {
      var res = films.Some(film => film.Contains("000"));

      Assert.False(res);
    }

    [Fact]
    public void ToArray_IIteratableToArray_ReturnTypeStringArray()
    {
      var res = films.ToArray();

      Assert.IsType<string[]>(res);
    }

    [Theory]
    [InlineData("Fear and Loathing in Las Vegas")]
    [InlineData("Fight Club")]
    [InlineData("Filth")]
    [InlineData("Trainspotting")]
    [InlineData("A Clockwork Orange")]
    [InlineData("RocknRolla")]
    [InlineData("Revolver")]
    [InlineData("Three Billboards Outside Ebbing, Missouri")]
    [InlineData("Pulp Fiction")]
    [InlineData("Dazed and Confused")]
    [InlineData("Lock, Stock and Two Smoking Barrels")]
    [InlineData("Inception")]
    [InlineData("Back to the Future")]
    [InlineData("Snatch.")]
    [InlineData("It's a Wonderful Life")]
    [InlineData("Fargo")]
    public void Concat_ConcatTwoIteratablesCheckIfAllItemsInside_ReturnArrayWithAllItems(string filmName)
    {
      CustomList<string> moreFilm = new CustomList<string>();
      moreFilm.AddRange(new string[] { "Inception", "Back to the Future", "Snatch.", "It's a Wonderful Life" });

      var concatFilms = films.Concat(moreFilm);

      moreFilm.Add("Fargo");
      var res = concatFilms.ToArray();

      Assert.Contains(filmName, res);
    }
  }
}
