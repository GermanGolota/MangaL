import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { ChapterComponent } from './components/chapter/chapter.component';
import { ChapterInfoComponent } from './components/chapter-info/chapter-info.component';
import { ChapterListComponent } from './components/chapter-list/chapter-list.component';



@NgModule({
  declarations: [ChapterComponent, ChapterInfoComponent, ChapterListComponent],
  imports: [
    CommonModule,
    FormsModule,
    AppRoutingModule
  ],
  exports: [ChapterListComponent]
})
export class ChapterModule { }
