using System;

namespace HalalGuide.Domain.Enum
{
	public enum CreateDiningResult
	{
		OK,
		AddressDoesNotExist,
		CouldNotCreateEntityInSimpleDB,
		CouldNotUploadPictureToS3,
		Error
	}
}

