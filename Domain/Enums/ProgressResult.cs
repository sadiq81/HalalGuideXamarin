using System;

namespace HalalGuide.Domain.Enums
{
	public class ProgressResult
	{
		public readonly string message;
		public readonly ProgressType type;

		ProgressResult (string message, ProgressType type)
		{
			this.message = message;
			this.type = type;
		}

		public static ProgressResult GetInstance (string message, ProgressType type)
		{
			return new ProgressResult (message, type);
		}
	}

	public enum ProgressType
	{
		Show,
		ShowWithText,
		ShowSuccessWithStatus,
		ShowErrorWithStatus,
		ShowToast,
		Dismiss
	}
}

