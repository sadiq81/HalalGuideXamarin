using System;

namespace HalalGuide.Domain.Enum
{
	public enum CreateEntityResult
	{
		OK = 0,
		CouldNotCreateEntityInSimpleDB = 1,
		CouldNotUploadReviewToS3 = 2,
		CouldNotUploadImageToS3 = 3,
		AddressDoesNotExist = 4,
		Error = 5,

	}
}

