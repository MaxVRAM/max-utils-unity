namespace MaxVram.Audio
{
    public class SoundSource : IMakeSound
    {
        protected SoundSource() { }
        
        public virtual float NextSample()
        {
            return 0;
        }
    }
}
