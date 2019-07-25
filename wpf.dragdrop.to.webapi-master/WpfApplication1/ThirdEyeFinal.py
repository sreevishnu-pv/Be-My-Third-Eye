
# coding: utf-8

# In[4]:


# Importing the required libraries
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
get_ipython().run_line_magic('matplotlib', 'inline')
import spacy
import pandas as pd
from pathlib import Path
nlp = spacy.load('en_core_web_sm', disable=['ner'])
merge_nps = nlp.create_pipe('sentencizer')
nlp.add_pipe(merge_nps, first = True)
merge_nps = nlp.create_pipe("merge_noun_chunks")
nlp.add_pipe(merge_nps)


# In[28]:


import os             
txt_files = os.listdir("C:\ThirdEye\Input")   # To Read all files from a location
print(txt_files) 


# In[29]:


import os  
tags_file = os.listdir("C:\ThirdEye\Tags")   # To Read all files from a location
print(tags_file ) 

with open('Tags.txt', 'r') as file:
    tags = file.read()
    
tags = tags[1:]
tags = tags[:-1]
print(tags)
tags = tags.replace("'","")
tags = tags.split(",")
#print(tags)
#print(tags[0])


# In[30]:


with open('newfile.txt','wb') as newf:
    for filename in txt_files:
        with open(filename,'rb') as hf:
            newf.write(hf.read())              

with open('newfile.txt', 'r') as file:
    text = file.read()
print(text)


# In[31]:


with open("newfile.txt", "r") as input:
    input_ = input.read().split("\n\n") 

ParaLength=(len(input_)-1)


# In[32]:


import re

def get_tags_text(EntSearchlist, text): 
            
    text = text.strip()   
    doc = nlp(text.lower())
    sents = list(doc.sents)
    
    df_sents_tags = pd.DataFrame(columns=['Sentence','TagsIdentified','Count','RowCount'])
    
    for index, sentence in enumerate(sents):
#         if index ==1 :
        print(sentence)
        hitEnts1 = []   
        entCount = []
        rowCount=[]
        # find brackets () {} []  in the string and replace it with ''
        string = sentence.string.translate({ord('{'): None, ord('}'): None, ord('('): None, ord(')'): None,
                                            ord('['): None, ord(']'): None})

        # Search the entities from EntSearchlist
        for word1 in EntSearchlist:
            count1 = sum(1 for _ in re.finditer(r'\b%s\b' % re.escape(word1), string))
            if count1 > 0:
                hitEnts1.append(word1)
                entCount.append(count1)
                
                rowCount=sum(entCount)            

        # store the details at sentence level       
        df_sents_tags.loc[index] = [string,hitEnts1,entCount,rowCount]

    return df_sents_tags
    


# In[33]:


def get_pargraph_score(sentenceScore):
        new_dataframe=sentenceScore[sentenceScore.iloc[:,-1].apply(lambda x: isinstance(x, (int, np.int64)))].sum()       
        return new_dataframe


# In[34]:


#Ppython program to check if two  
# to get unique values from list 
# using numpy.unique  
import numpy as np 
  
# function to get unique values 
def unique(list1): 
    x = np.array(list1) 
    return np.unique(x)


# In[35]:


#To Find Scores of paragraph
df_paragraph_score = pd.DataFrame(columns=['Paragraph','Score','IdendifiedTags','UniqueTags']) 
for i in  range(ParaLength):
    paratext=input_[i]
    sentenceScore=get_tags_text(tags,paratext)
    paragraphScore=get_pargraph_score(sentenceScore)      
    df=pd.DataFrame(paragraphScore) 
    if df.empty:
        Score=0
        IdendifiedTags='None'
        uniqueTags='None'
        df_paragraph_score.loc[i] = [paratext,Score,IdendifiedTags,UniqueTags]
    else:
        Score=df[0].iloc[3]
        IdendifiedTags=df[0].iloc[1] 
        UniqueTags=unique(IdendifiedTags) 
        df_paragraph_score.loc[i] = [paratext,Score,IdendifiedTags,UniqueTags]
    del df
    
df_paragraph_score


# In[37]:


df_paragraph_score.sort_values(["Score"], ascending=False, inplace=True)
df_paragraph_score.head()
df_final=df_paragraph_score.head()
df_final.to_csv("thirdeye_output.csv",index=False)
df_final
                

