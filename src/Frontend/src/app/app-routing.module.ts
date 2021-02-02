import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChapterComponent } from './chapter/components/chapter/chapter.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { RandomMangaComponent } from './manga/components/random-manga/random-manga.component';

const routes: Routes = [
  {
    path:'',
    component: LandingPageComponent
  },
  {
    path:'random',
    component: RandomMangaComponent
  },
  {
    path:'chapter/:chapterId',
    component: ChapterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
