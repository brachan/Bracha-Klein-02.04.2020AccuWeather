import { Component, Input, OnInit } from '@angular/core';
import { LocationService } from '../../services/location.service';
import { WeatherService } from '../../services/weather.service';

@Component({
  selector: 'current-conditions',
  templateUrl: './current-conditions.component.html',
  styleUrls: ['./current-conditions.component.css']
})
export class CurrentConditionsComponent implements OnInit {
  condition: any;
  metric: boolean;
  constructor(private weatherService: WeatherService,private locationService: LocationService) { }

  ngOnInit() {
    this.locationService.currentCityCode.subscribe(cityKey => {
      this.weatherService.getCurrnetCondition(cityKey).subscribe(data =>
      {
        this.condition = data[0];
      },
        error => { console.log(error) })
    });
    this.weatherService.metric.subscribe(metric => {
      this.metric = metric;
    });
  }

}
