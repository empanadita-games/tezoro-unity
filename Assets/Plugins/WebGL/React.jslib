mergeInto(LibraryManager.library, {
  TrySyncWallet: function (userName, score) {
    window.dispatchReactUnityEvent("TrySyncWallet", UTF8ToString(userName), score);
  },
});