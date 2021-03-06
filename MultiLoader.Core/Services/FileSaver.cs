﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MultiLoader.Core.Db;
using MultiLoader.Core.Infrustructure;
using MultiLoader.Core.Model;

namespace MultiLoader.Core.Services
{
    public class FileSaver : IContentSaver
    {
        private readonly string _saveDir;
        public event EventHandler<Content> OnSave;
        public event EventHandler<Exception> OnSaveError;
        
        public FileSaver(string saveDir)
        {
            _saveDir = saveDir;

            if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
        }

        public void SaveContent(Content content)
        {
            if (content == null || content.Data.Length == 0) return;

            try
            {
                var saveFilePath = Path.Combine(_saveDir, content.ContentMetadata.Name);
                File.WriteAllBytes(saveFilePath, content.Data);
                OnSave?.Invoke(this, content);
            }
            catch(Exception ex)
            {
                OnSaveError?.Invoke(this, ex);
            }
        }

        public IRepository<ContentMetadata> GetContentMetadataRepository()
        {
            return new LiteDbRepository<ContentMetadata>(_saveDir);
        }
    }
}