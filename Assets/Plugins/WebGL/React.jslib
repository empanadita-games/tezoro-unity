mergeInto(LibraryManager.library, {
  TrySyncWallet: function {
    window.dispatchReactUnityEvent("TrySyncWallet");
  },
});
mergeInto(LibraryManager.library, {
  GetTezos: function {
    window.dispatchReactUnityEvent("GetTezos", amount);
  },
});