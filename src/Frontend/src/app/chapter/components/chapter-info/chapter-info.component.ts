import { Component, Input, OnInit } from '@angular/core';
import { ChapterInfoModel } from 'src/app/shared/Models/IManga';

@Component({
  selector: 'app-chapter-info',
  templateUrl: './chapter-info.component.html',
  styleUrls: ['./chapter-info.component.scss']
})
export class ChapterInfoComponent{
  @Input() chapter:ChapterInfoModel;

}
