using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruYumWebAPI.Model
{
    public class MenuItem
    {
        public int UserStoryId { get; set; }
        public string UserStoryName { get; set; }
        public string StoryOwner { get; set; }
        public int StoryPoints { get; set; }
        public string StoryDescription { get; set; }
    }
}
