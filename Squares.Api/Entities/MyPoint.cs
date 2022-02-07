namespace Squares.Api.Entities
{
    public class MyPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() => $"{X} :\t {Y}";
    }
}
