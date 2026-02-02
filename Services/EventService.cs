using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EventEaseApp.Services
{
	public class EventItem
	{
		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		public DateTime? Date { get; set; }

		[Required]
		public string Location { get; set; } = string.Empty;

		public List<Participant> Participants { get; set; } = new();
	}

	public class Participant
	{
		[Required(ErrorMessage = "Participant name is required")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Participant email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = string.Empty;
	}

	public class EventService
	{
		private readonly List<EventItem> _events = new()
		{
			new EventItem { Name = "Community Meetup", Date = DateTime.Now.AddDays(7), Location = "City Hall", Participants = new List<Participant>
				{
					new Participant { Name = "Alice Smith", Email = "alice@example.com" }
				}
			},
			new EventItem { Name = "Tech Conference", Date = DateTime.Now.AddMonths(1), Location = "Convention Center", Participants = new List<Participant>() },
			new EventItem { Name = "Art Workshop", Date = DateTime.Now.AddDays(14), Location = "Art Studio", Participants = new List<Participant>() }
		};

		public IEnumerable<EventItem> GetEvents()
		{
			return _events;
		}

		public void AddEvent(EventItem item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			_events.Add(item);
		}

			public bool AddParticipant(string eventName, Participant participant)
			{
				if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentException("eventName is required", nameof(eventName));
				if (participant == null) throw new ArgumentNullException(nameof(participant));

				var ev = _events.FirstOrDefault(e => string.Equals(e.Name, eventName, StringComparison.OrdinalIgnoreCase));
				if (ev == null) return false;

				ev.Participants.Add(participant);
				return true;
			}

			public IEnumerable<Participant> GetParticipants(string eventName)
			{
				if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentException("eventName is required", nameof(eventName));

				var ev = _events.FirstOrDefault(e => string.Equals(e.Name, eventName, StringComparison.OrdinalIgnoreCase));
				return ev?.Participants ?? Enumerable.Empty<Participant>();
			}

        public bool RemoveEvent(EventItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return _events.Remove(item);
        }
	}
}

