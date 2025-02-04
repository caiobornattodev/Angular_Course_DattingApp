import { Photo } from "./photo"

export interface Member {
  id: number
  userName: string
  age: number
  photoUrl: any
  knownAs: string
  created: string
  lastActive: string
  gender: string
  intruduction: any
  intrests: any
  lookingFor: string
  city: string
  country: string
  photos: Photo[]
}
