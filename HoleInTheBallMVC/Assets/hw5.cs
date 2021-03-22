using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class TestExtension
{
    public static int CountCharacter(this string str, char c)
    {
        return str.Where(s => s == c).Count();
    }
}

public class hw5 : MonoBehaviour
{

    private void Start()
    {
        Debug.Log($"HW5_2");
        HW5_2();
        Debug.Log("");
        Debug.Log($"HW5_3");
        HW5_3();
        Debug.Log("");
        Debug.Log($"HW5_4");
        HW5_4();
    }

    private void HW5_2()
    {
        //2.	Реализовать метод расширения для поиска количество символов в строке

        string s = "dwcwvcrvrtbrbqwfdrgrnbyntmtnt";
        Debug.Log($"In the '{s}' line of {s.CountCharacter('d')} characters 'd'");
    }

    private void HW5_3()
    {
        //3.Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
        //c.  * *используя Linq.

        List<int> data = new List<int> { 1, 2, 3, 2, 23, 2, 23, 23, 321, 4, 312, 1, 12123, 3, 23 };

        var res = data.GroupBy(q => q);
        foreach (var resOne in res)
        {
            Debug.Log($"Data: {resOne.Key,7}   Count: {resOne.Count()}");
        }
    }


    private void HW5_4()
    {
        //4a. Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
        Dictionary<string, int> dict = new Dictionary<string, int>()
        {
            {"four",4 },
            {"two",2 },
            { "one",1 },
            {"three",3 },
        };
        var d = dict.OrderBy(x => x.Value);

        foreach (var pair in d)
        {
            Debug.Log($"{pair.Key} - {pair.Value}");
        }

    }

}
