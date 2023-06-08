import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

const routes: Routes = [
  {
    path: 'identity',
    loadChildren: () =>
      import('./identity/identity.module').then((m) => m.IdentityModule),
  },
  {
    path: 'recommended',
    loadChildren: () =>
      import('./recommended/recommended.module').then(
        (m) => m.RecommendedModule,
      ),
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
