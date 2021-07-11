﻿namespace GDShrapt.Reader
{
    public sealed class GDEnumValuesList : GDCommaSeparatedList<GDEnumValueDeclaration>,
        ITokenReceiver<GDEnumValueDeclaration>,
        ITokenReceiver<GDComma>
    {
        internal override GDReader ResolveNode()
        {
            var node = new GDEnumValueDeclaration();
            ListForm.Add(node);
            return node;
        }

        internal override bool IsStopChar(char c)
        {
            return !c.IsIdentifierStartChar();
        }

        public override GDNode CreateEmptyInstance()
        {
            return new GDEnumValuesList();
        }

        void ITokenReceiver<GDEnumValueDeclaration>.HandleReceivedToken(GDEnumValueDeclaration token)
        {
            ListForm.Add(token);
        }

        void ITokenReceiver<GDComma>.HandleReceivedToken(GDComma token)
        {
            ListForm.Add(token);
        }
    }
}
