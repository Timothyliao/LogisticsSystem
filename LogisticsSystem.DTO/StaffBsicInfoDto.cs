using System;

namespace LogisticsSystem.DTO
{
    public class StaffBsicInfoDto
    {
        public string Name { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public string IdCard { get; set; }

        public string Position { get; set; }

        public string Address { get; set; }

        public Guid SectionId { get; set; }
    }
}
