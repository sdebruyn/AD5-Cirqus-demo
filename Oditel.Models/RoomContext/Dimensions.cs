namespace Oditel.Models.RoomContext
{
    public sealed class Dimensions
    {
        public Dimensions()
        {
        }

        public Dimensions(decimal width, decimal length, decimal height)
        {
            Width = width;
            Length = length;
            Height = height;
        }

        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }

        public override string ToString()
        {
            return $"Height: {Height}, Length: {Length}, Width: {Width}";
        }
    }
}