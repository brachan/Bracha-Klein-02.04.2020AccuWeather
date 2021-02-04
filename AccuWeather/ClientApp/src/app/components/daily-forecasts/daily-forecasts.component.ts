import { Component, OnInit } from '@angular/core';
import { LocationService } from '../../services/location.service';
import { WeatherService } from '../../services/weather.service';

@Component({
  selector: 'daily-forecasts',
  templateUrl: './daily-forecasts.component.html',
  styleUrls: ['./daily-forecasts.component.css']
})
export class DailyForecastsComponent implements OnInit {
  dailyForecasts : any[] = [];

  constructor(private weatherService: WeatherService, private locationService: LocationService) { }

  ngOnInit() {
    this.locationService.currentCityCode.subscribe(cityKey => {
      this.weatherService.getDailyForecasts(cityKey).subscribe(data => {
        this.dailyForecasts = data.DailyForecasts;
      },
        error => { console.log(error) })
    });
    this.weatherService.metric.subscribe(metric => {
      if (this.locationService.currentCityCode) {
        this.weatherService.getDailyForecasts(this.locationService.currentCityCode.value).subscribe(data => {
          this.dailyForecasts = data.DailyForecasts;
        }, error => { console.log(error) })
      }
    });
  }
}
