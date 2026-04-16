namespace GeekCliServices.Services.Rx.Models
{
    public record RxCommand
    {
        public string Name { get; init; }

        public bool Flat { get; init; }

        public RxCommand(string name, bool flat)
        {
            Name = name;
            Flat = flat;
        }
    }
}
