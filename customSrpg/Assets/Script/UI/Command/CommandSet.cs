using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public partial class CommandBase : MonoBehaviour
    {
        public abstract class CommandSet
        {
            public abstract void Decide(CommandBase owner);
            public abstract void Cancel(CommandBase owner);
        }
        public class DecInCanOut : CommandSet
        {
            public override void Decide(CommandBase owner) => owner.MoveChild();
            public override void Cancel(CommandBase owner) => owner.MoveParent();
        }
        public class DecMessCanOut : CommandSet
        {
            public override void Decide(CommandBase owner) => owner.OpenMessage();
            public override void Cancel(CommandBase owner) => owner.MoveParent();
        }
        public class DecOn : CommandSet
        {
            public override void Decide(CommandBase owner) => owner.CommandDecide();
            public override void Cancel(CommandBase owner) { }
        }
        public class DecOut : CommandSet
        {
            public override void Decide(CommandBase owner) => owner.MoveParent();
            public override void Cancel(CommandBase owner) { }
        }
    }
}
