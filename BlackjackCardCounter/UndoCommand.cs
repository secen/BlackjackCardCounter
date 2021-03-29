using System;

namespace BlackjackCardCounter
{
    public class UndoCommand
    {
        public Action<Card> action;
        public Card argument;
    }
}