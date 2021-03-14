using System;
using AniraSP.DAL.Domain;
using AniraSP.DAL.Domain.Sites;

namespace AniraSP.WebUI.Pages.Sites.SiteEdit {
    public class SiteEditViewDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Shard { get; set; }
        public int NumberOfThreads { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }

        public DateTime StartTime {
            get => new(2000, 01, 01, StartHour, StartMinute, 0);
            set {
                StartHour = value.Hour;
                StartMinute = value.Minute;
            }
        }


        public SiteEditViewDto(Site siteDb) {
            Id = siteDb.Id;
            Name = siteDb.Name;
            Url = siteDb.SiteUrl;
            IsActive = siteDb.IsActive;
            Shard = siteDb.Shard;
            NumberOfThreads = siteDb.SettingsData?.ParsingSettings?.NumberOfTread ?? 1;
            StartHour = siteDb.SettingsData?.ParsingSettings?.StartHour ?? 0;
            StartMinute = siteDb.SettingsData?.ParsingSettings?.StartMinute ?? 0;
        }

        public Site ToSite() {
            return new() {
                Id = Id,
                Name = Name,
                SiteUrl = Url,
                IsActive = IsActive,
                Shard = Shard,
                SettingsData = new SiteSettings {
                    ParsingSettings = new ParsingSettings {
                        StartHour = StartHour,
                        StartMinute = StartMinute,
                        NumberOfTread = NumberOfThreads
                    }
                }
            };
        }
    }
}