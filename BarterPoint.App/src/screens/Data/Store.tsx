import {configureStore} from "@reduxjs/toolkit"
import productsReducer from "./ProductSlice"
import bidsReducer from "./BidSlice"
import usersReducer from "./UserSlice"

const store = configureStore({
  reducer: {
    products: productsReducer,
    bids: bidsReducer,
    users: usersReducer,
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export default store
