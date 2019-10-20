
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class ProductSubCategoryDto : EntityDto
    {
		public string PipeDiameter { get; set; }

		public string WallS { get; set; }

		public string SDR { get; set; }

		public string PN { get; set; }

		public string WPM { get; set; }

		public string OutputKGH { get; set; }

		public string HauiOffSpeed { get; set; }

		public string OutputTA { get; set; }

		public string ProductionTimeKMA { get; set; }

		public string ProductionPipeLengthKMA { get; set; }

		public string PipeLengthM { get; set; }

		public string Extruder { get; set; }

		public string PipeHead { get; set; }

		public string UnitPrice { get; set; }


		 public int ProductCategoryId { get; set; }

		 
    }
}