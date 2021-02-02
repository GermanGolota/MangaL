import { Component, Input} from '@angular/core';
import { IManga } from 'src/app/shared/Models/IManga';

@Component({
  selector: 'app-manga-info',
  templateUrl: './manga-info.component.html',
  styleUrls: ['./manga-info.component.scss']
})
export class MangaInfoComponent{
  @Input() manga:IManga;
}
