**Playing with objects**
```
"Closer to real life?"
"Extensible?"
...
```
**A solution in a few lines, but ...**
```
public int[] FindOrder(int numCourses, int[][] prerequisites)
{
    if (prerequisites.Length == 0) return Enumerable.Range(0, numCourses).ToArray();
    var courses = Enumerable.Range(0, numCourses).Select(x => new Course(x)).ToArray();
    Array.ForEach(prerequisites, pre => courses[pre[0]].Prerequisites.Add(courses[pre[1]])); 
    Array.Sort(courses);
    return courses[^1].Position > numCourses + 1 
	    ? Array.Empty<int>() 
		: courses.Select(x => x.Key).ToArray();
}
```
```
public class Course : IComparable<Course>
{
    private double _position;

    public int Key;
    public List<Course> Prerequisites;
    public static Stack<int> PrerequisiteTo;
    public int CompareTo(Course other) => Position.CompareTo(other.Position);
    public static double Epsilon = 0.0001;

    public Course(int key)
    {
        Key = key;
        Position = -1.0;
        Prerequisites = new List<Course>();
        PrerequisiteTo = new Stack<int>();
    }

    public double Position
    {
        get
        {
            if (_position >= Epsilon) return _position;
            if (!Prerequisites.Any()) return _position = Key + 1;
            if (Prerequisites.Any(x => PrerequisiteTo.Contains(x.Key))) 
			    return _position = double.MaxValue - 1.0;
                
            PrerequisiteTo.Push(Key);
            _position = Math.Max(Key + 1, Prerequisites.Max(x => x.Position) + Epsilon);
            PrerequisiteTo.Pop();

            return _position;
        }
        set => _position = value;
    }
}
```
**If prerequisites is empty**
**.... Then return the array of range numbers**
```
if (prerequisites.Length == 0) return Enumerable.Range(0, numCourses).ToArray();
```
**Else**
**.... Populate Course array by range numbers**
```
var courses = Enumerable.Range(0, numCourses).Select(x => new Course(x)).ToArray();
```
**Iterate prerequisites array**
**Find the course**
**Add the prerequisite**
```
Array.ForEach(prerequisites, pre => courses[pre[0]].Prerequisites.Add(courses[pre[1]])); 
```
**IComparable.CompareTo() is used to sort Course array**

```
...
Array.Sort(courses);
...
"Following statement shows how the array is sorted"
...
Array.Sort(courses, (x, y) => x.CompareTo(y));
...
"return +1 for Greater Than"
"return -1 for Less Than"
"return 0 for Equal"
```
```
"Compare the position of this course with the other one"
...
public int CompareTo(Course other) => Position.CompareTo(other.Position);
...
```
**Check the implementation of Course**
**A position can not be greater than the number of courses plus one (n + 1)
Except cycled collections**
```
return courses[^1].Position > numCourses + 1 
    ? Array.Empty<int>() 
    : courses.Select(x => x.Key).ToArray();
```
**Getters and setters are ignored**
```
"The name of this field conflicts with many naming conventions, anyway ..."
public int Key;
```
**Key is also the index of the Course**
```
public int Key;
```
**PrerequisiteTo means Parents**
**Because of it is a collection, its name shuld have been plural**
**But ...**
```
public static Stack<int> PrerequisiteTo;
```
**Every course contains its prerequisites
So it is easy to add some other operations and conditions**
```
public List<Course> Prerequisites;
```
**Courses are sorted by Position field**
```
"-1 means >> Position is not calculated yet"
"This is critical!"
"Because calculating it is expensive"
"If it is calculated, return it, If not, calculate it"
...
Position = -1.0;
...
```

**Epsilon is added to the maximum position of prerequisites
A micro increment**
```
"The (min) positon of a course must be greater than the max position of its prerequisites"
"Decrease the value of Epsilon for larger data sets"
"This one was enough for the challenge"
...
public static double Epsilon = 0.0001;
...
"If the key of the course is greater than the max position of its prerequisites"
      "Then assign Key to Position"
"Otherwise"
	"Assign the (Max + Epsilon) to Position"
...
_position = Math.Max(Key + 1, Prerequisites.Max(x => x.Position) + Epsilon);
...
```
**Is there any valid schedule?**
```
"This is just another method"
"Push current course to the stack, before checking its prerequisites"
"Stack is a LIFO(Last In First Out) collection"

"If you walk"
...
"Portugal" 
"Portugal"
...
"Portugal"
	"Spain"
		"France"
			"Belgium"
				"Holland"
				"Holland"
			"Belgium"
		"France"
	"Spain"
"Portugal"
...
"Then"
"People would like to fly like birds"
...
PrerequisiteTo.Push(Key);
_position = Math.Max(Key + 1, Prerequisites.Max(x => x.Position) + Epsilon);
PrerequisiteTo.Pop();
...
```
**A position that is greater than or equal to Epsilon means already calculated one**
```
if (_position >= Epsilon) return _position;
```
**If the course does not have any prerequisites then use its Key**
```
if (!Prerequisites.Any()) return _position = Key + 1;
```
**ThisDate is greater(later) than ThatDate**
**On the other hand**
**ThatDate is greater(later) than ThisDate
!!!**
```
"double.MaxValue means !!! Invalid Position !!!"
"Check it later"
...
if (Prerequisites.Any(x => PrerequisiteTo.Contains(x.Key))) 
    return _position = double.MaxValue - 1.0
...
"Another method is throwing an exception"
"And other ways ..."
```
**Return**
```
"Could not find the object-comparison hashtag"
	"Dear LeetCoders?"
"I am open to any kind of feedback || Void"
```