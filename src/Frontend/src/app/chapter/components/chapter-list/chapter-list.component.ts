import { Component, Input } from '@angular/core';
import { ChapterInfoModel } from 'src/app/shared/Models/IManga';

@Component({
  selector: 'app-chapter-list',
  templateUrl: './chapter-list.component.html',
  styleUrls: ['./chapter-list.component.scss']
})
export class ChapterListComponent{

  @Input() chapters:Array<ChapterInfoModel>;
}
