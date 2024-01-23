using System.Diagnostics;

namespace BraillePixelEditor
{
    internal class ActionManager
    {
        private List<Action> actionBuffer = new();

        public void push(Action action) {
            if (actionBuffer.Count < 30)
                actionBuffer.Add(action);
            else {
                Debug.Print("actionBuffer is full!");

                shift();
                actionBuffer.Add(action);
            }
        }

        public Action? shift() {
            if (actionBuffer.Count == 0) return null;

            var item = actionBuffer[0];
            actionBuffer.RemoveAt(0);
            return item;
        }

        public Action? pop() {
            if (actionBuffer.Count == 0) return null;

            var item = actionBuffer[^1];
            actionBuffer.RemoveAt(actionBuffer.Count - 1);
            return item;
        }
    }

    enum ActionTypes {
        None,
        PutPixel,
        BucketFill
    }

    class Action {
        public ActionTypes Type;

        /// <summary>
        /// Only for PutPixel.
        /// </summary>
        public Point? position;

        /// <summary>
        /// The state of pixel before being replaced with put pixel.
        /// </summary>
        public Color? lastColour;

        /// <summary>
        /// The state of bitmap before being replaced with bucket fill.
        /// </summary>
        public Bitmap? lastState;
    }
}
