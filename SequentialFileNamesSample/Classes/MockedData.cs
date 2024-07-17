using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SequentialFileNamesSample.Modals;

namespace SequentialFileNamesSample.Classes;
internal class MockedData
{
    public static List<Person> PeopleMocked() =>
    [
        new()
        {
            PersonId = 1,
            FirstName = "Karen",
            LastName = "Payne",
            BirthDate = new DateOnly(1956, 9, 24),
            TimeOnly = new TimeOnly(13, 0)
        },

        new()
        {
            PersonId = 2,
            FirstName = "Mary",
            LastName = "Adams",
            BirthDate = new DateOnly(1959, 3, 24),
            TimeOnly = new TimeOnly(14, 30)
        },

        new()
        {
            PersonId = 3,
            FirstName = "Jim",
            LastName = "Smith",
            BirthDate = new DateOnly(1987, 3, 15),
            TimeOnly = new TimeOnly(3, 10)
        }
    ];
}
