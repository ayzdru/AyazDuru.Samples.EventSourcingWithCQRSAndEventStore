using System;
using System.Collections.Generic;
using System.Text;

public partial class Constants
{
    public static class TodoListItem
    {
        public const int TitleMinimumLength = 3;
        public const int TitleMaximumLength = 500;
        public const bool TitleRequired = true;

        public const int DescriptionMinimumLength = 3;
        public const int DescriptionMaximumLength = 500;
        public const bool DescriptionRequired = true;

        public const bool IsDoneRequired = true;
    }
}
