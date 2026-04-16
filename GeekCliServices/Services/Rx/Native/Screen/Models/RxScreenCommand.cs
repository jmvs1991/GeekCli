using GeekCliServices.Services.Rx.Models;

namespace GeekCliServices.Services.Rx.Native.Screen.Models
{
    public sealed record RxScreenCommand : RxCommand
    {
        public bool Schema { get; set; }

        public bool Wrapper { get; set; }

        public RxScreenCommand(string name, bool flat, bool schema, bool wrapper) : base(name, flat)
        {
            Schema = schema;
            Wrapper = wrapper;
        }
    }
}
