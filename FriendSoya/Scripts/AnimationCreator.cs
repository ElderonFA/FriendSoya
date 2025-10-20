using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FriendSoya.Scripts
{
    public static class AnimationCreator
    {
        public static DoubleAnimation CreateScaleDoubleAnim(double from, double to, TimeSpan duration, bool reverse, bool isForeverAnim = false)
        {
            var anim = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = duration,
                AutoReverse = reverse,
            };

            if (isForeverAnim)
            {
                anim.RepeatBehavior = RepeatBehavior.Forever;
            }

            return anim;
        }

        public static ColorAnimation CreateColorDoubleAnim(Color toColor, TimeSpan duration, bool reverse, bool isForeverAnim = false)
        {
            var anim = new ColorAnimation
            {
                To = toColor,
                Duration = duration,
                AutoReverse = reverse,
                RepeatBehavior = RepeatBehavior.Forever
            };

            if (isForeverAnim)
            {
                anim.RepeatBehavior = RepeatBehavior.Forever;
            }

            return anim;
        }
    }
}
