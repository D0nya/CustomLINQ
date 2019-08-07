using System;
using System.Collections.Generic;

namespace Iterator
{
  public interface IIteratable<T>
  {
    IEnumerator<T> GetEnumerator();
  }
}
