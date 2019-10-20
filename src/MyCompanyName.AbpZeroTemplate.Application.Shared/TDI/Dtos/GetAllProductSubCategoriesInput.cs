using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllProductSubCategoriesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PipeDiameterFilter { get; set; }

		public string WallSFilter { get; set; }

		public string SDRFilter { get; set; }

		public string PNFilter { get; set; }

		public string WPMFilter { get; set; }

		public string OutputKGHFilter { get; set; }

		public string HauiOffSpeedFilter { get; set; }

		public string OutputTAFilter { get; set; }

		public string ProductionTimeKMAFilter { get; set; }

		public string ProductionPipeLengthKMAFilter { get; set; }

		public string PipeLengthMFilter { get; set; }

		public string ExtruderFilter { get; set; }

		public string PipeHeadFilter { get; set; }

		public string UnitPriceFilter { get; set; }


		 public string ProductCategoryMaterialFilter { get; set; }

		 
    }
}