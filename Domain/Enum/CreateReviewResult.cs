using System;

namespace HalalGuide.Domain.Enum
{
	public enum CreateReviewResult
	{
		OK,
		CouldNotCreateEntityInSimpleDB,
		CouldNotUploadReviewToS3,
		Error
	}
}

