using System;
using Xunit;

namespace Iterator.Tests
{
  public class CustomListTest
  {
    [Fact]
    public void Count_Add3ItemsAndCount_Return3()
    {
      CustomList<int> intList = new CustomList<int>();
      intList.AddRange(new int[] { 1,2,3});

      var res = intList.Count;

      Assert.Equal(3, res);
    }

    [Fact]
    public void Count_ClearListAndCount_Return0()
    {
      CustomList<int> intList = new CustomList<int>();
      intList.Add(133);
      intList.Clear();

      var res = intList.Count;

      Assert.Equal(0, res);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public void Indexer_IndexLessThan0OrMoreThanCount_IndexOutOfRangeException(int index)
    {
      CustomList<int> intList = new CustomList<int>();

      Assert.Throws<ArgumentOutOfRangeException>(() => intList[index]);
    }

    [Fact]
    public void Indexer_IndexBetween0AndCount_ReturnValue()
    {
      CustomList<int> intList = new CustomList<int>();
      intList.Clear();
      intList.AddRange(new int[] { 1, 5, 8, 2, 6, 9, 3, 7, 0 });

      var res = intList[5];

      Assert.Equal(9, res);
    }
  }
}
