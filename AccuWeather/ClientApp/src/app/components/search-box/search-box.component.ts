import { Component, OnInit } from '@angular/core';
import { LocationService } from '../../services/location.service';
import { WeatherService } from '../../services/weather.service';

@Component({
  selector: 'search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})
export class SearchBoxComponent implements OnInit {
  text: string;

  results: string[];
  constructor(private locationService: LocationService, private weatherService: WeatherService) { }

  ngOnInit() {
  }

  
  search(event) {
    this.locationService.getLocationsAutoComplete(event.query).subscribe(data => {
      data = data.map(obj => ({ ...obj, description: obj.LocalizedName + ', ' + obj.Country.LocalizedName}))
      this.results = data;
    }, error => console.error(error));
  }

  onSelectedCity(event) {
    this.locationService.currentCityCode.next(Number(event.Key));
  }

  changeUnit() {
    this.weatherService.metric.next(!this.weatherService.metric.value);
    if (this.locationService.currentCityCode) {
      this.weatherService.getDailyForecasts(this.locationService.currentCityCode.value)
    }
  }
}
