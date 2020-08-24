using System;
using System.Collections.Generic;

namespace Mvx.Plugin.Style.SampleApp {
	public class StoryService :IStoryService{

		public List<Story> GetStories() {
			List<Story> stories = new List<Story>();
            //< a > ww km </ a >
			var eightyDays = new Story() {Title="Jules Verne 80 days around the world", Subtitle = "Jules Verne" };//Around the world in 80 days
            eightyDays.Paragraph = "That gentleman was really <a>t est</a>";// , and that at the <a href='http://www.landal.nl/algemeen/privacy/app'>moment</a> when he was about to <a href=\"https://www.google.nl/maps/\">attain</a> his end. This <a href='https://www.google.nl/maps/'>arrest</a> was fatal. Having arrived at <b>Liverpool</b> at twenty minutes before twelve on the 21st of December, he had till a quarter before nine that evening to reach the Reform Club, that is, nine hours and a quarter; the journey from <b>Liverpool</b> to London was six hours.\r\n\r\n<i>If anyone, at this moment, had entered the Custom House, he would have found Mr. Fogg seated, motionless, calm, and without apparent anger, upon a wooden bench.</i>\r\n\r\nHe was not, it is true, resigned; but this last blow failed to force him into an outward betrayal of any emotion. Was he being devoured by one of those secret rages, all the more terrible because contained, and which only burst forth, with an irresistible force, at the last moment? No one could tell. There he sat, calmly waiting—for what? Did he still cherish hope? Did he still believe, now that the door of this prison was closed upon him, that he would succeed?";
			stories.Add(eightyDays);

			var gulliversTravels = new Story() { Title = "Gulliver's Travels", Subtitle = "Jonathan Swift" };
			gulliversTravels.Paragraph = "At the place where the carriage stopped there stood an ancient temple, esteemed to be the largest in the whole kingdom; which, having been polluted some years before by an unnatural murder, was, according to the zeal of those people, looked upon as profane, and therefore had been applied to common use, and all the ornaments and furniture carried away. In this edifice it was determined I should lodge. The great gate fronting to the north was about four feet high, and almost two feet wide, through which I could easily creep. On each side of the gate was a small window, not above six inches from the ground: into that on the left side, the king's smith conveyed four-score and eleven chains, like those that hang to a lady's watch in Europe, and almost as large, which were locked to my left leg with six-and-thirty padlocks. Over against this temple, on the other side of the great highway, at twenty feet distance, there was a turret at least five feet high. Here the emperor ascended, with many principal lords of his court, to have an opportunity of viewing me, as I was told, for I could not see them. ";
			stories.Add(gulliversTravels);

			var miles = new Story() { Title = "Twenty thousand leagues under the sea", Subtitle = "Jules Verne" };
			miles.Paragraph = "We had at last arrived on the borders of this forest, doubtless one of the finest of Captain Nemo's immense domains. He looked upon it as his own, and considered he had the same right over it that the first men had in the first days of the world. And, indeed, who would have disputed with him the possession of this submarine property? What other hardier pioneer would come, hatchet in hand, to cut down the dark copses?\n\nThis forest was composed of large tree-plants; and the moment we penetrated under its vast arcades, I was struck by the singular position of their branches—a position I had not yet observed.";
			stories.Add(miles);
			return stories;
		}
	}
}
